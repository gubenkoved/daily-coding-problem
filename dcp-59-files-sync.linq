<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

// This problem was asked by Google.
// 
// Implement a file syncing algorithm for two computers over
// a low-bandwidth network. What if we know the files in the
// two computers are mostly the same?

void Main()
{
	// the main idea is to use block hashing
	// each file can be split into the chunks of some size (e.g. 1MB)
	// and hashed; in order to incrementaly sync we can has the target
	// and only transfer and sync blocks with difference
	
	// Links:
	// https://en.wikipedia.org/wiki/Rsync#Variations
	// https://stackoverflow.com/questions/7060685/how-does-dropbox-synchronization-work
	
	var source = new LocalFSFileProvider(@"C:\Users\nrj\Desktop\source");
	var target = new LocalFSFileProvider(@"C:\Users\nrj\Desktop\target");
	
	var syncEngine = new SyncEngine();
	
	syncEngine.SyncOneWay(source, target);
}

public class SyncFileInfo
{
	public string RelativePath { get; set; }
}

public class SyncBlockMetaInfo
{
	public long Offset { get; set; }
	public int Length { get; set; }
	public byte[] Hash { get; set; }
}

public class SyncBlockData
{
	public byte[] Data { get; set; }
}

public class SyncEngine
{
	public int BlockSize { get; set; } = 1024 * 1024; // 1 MB
	
	public void SyncOneWay(ISyncFileProvider source, ISyncFileProvider target)
	{
		// get the files metadata from the source and target
		// add missing files, sync files present in both
		
		// get all the files in the source
		SyncFileInfo[] targetFiles = target.GetFiles().ToArray();
		
		HashSet<SyncFileInfo> matchedTargetFiles = new HashSet<SyncFileInfo>();
		
		foreach (SyncFileInfo sourceFile in source.GetFiles())
		{
			Util.Metatext($"Syncing file '{sourceFile.RelativePath}'...").Dump();
			
			SyncBlockMetaInfo[] sourceBlocks = source.GetFileBlocks(sourceFile.RelativePath, BlockSize).ToArray();
			
			SyncFileInfo targetFile = targetFiles.FirstOrDefault(x => x.RelativePath == sourceFile.RelativePath);
			
			SyncBlockMetaInfo[] targetBlocks;

			if (targetFile != null)
			{
				matchedTargetFiles.Add(targetFile);
				targetBlocks = target.GetFileBlocks(sourceFile.RelativePath, BlockSize).ToArray();
			}
			else
			{
				targetBlocks = new SyncBlockMetaInfo[] { };
			}
				
			for (int blockIdx = 0; blockIdx < sourceBlocks.Length; blockIdx++)
			{
				SyncBlockMetaInfo sourceBlock = sourceBlocks[blockIdx];

				Util.Metatext($" Handling block #{blockIdx} (offset {sourceBlock.Offset}, len {sourceBlock.Length})...").Dump();
				
				// sync per block if hash different
				bool sync = true;

				if (targetBlocks.Length >= blockIdx + 1 // block present
					&& HashEquals(sourceBlocks[blockIdx].Hash, targetBlocks[blockIdx].Hash)) // hashes are the same!
				{
					sync = false;
				}
				
				if (sync)
				{
					Util.Metatext($"  Syncing block...").Dump();
					
					// read block
					SyncBlockData data = source.ReadBlock(sourceFile.RelativePath, sourceBlocks[blockIdx].Offset, sourceBlocks[blockIdx].Length);
					
					// write block
					target.WriteBlock(sourceFile.RelativePath, data.Data, sourceBlocks[blockIdx].Offset);
				} else
				{
					// blocks are equals! skip
					
					Util.Metatext($"  Skip block as it's the same").Dump();
				}
				
				long sourceFullLen = sourceBlocks.Sum(x => (long) x.Length);
				long targetFullLen = targetBlocks.Sum(x => (long) x.Length);
				
				if (targetFullLen > sourceFullLen)
				{
					// cut target file
					Util.Metatext($"  Cutting target file as it's longer than source").Dump();
					
					target.SetFileLength(sourceFile.RelativePath, sourceFullLen);
				}
			}
		}
		
		foreach (SyncFileInfo targetFile in targetFiles)
		{
			if (!matchedTargetFiles.Contains(targetFile))
				target.RemoveFile(targetFile.RelativePath);
		}
	}
	
	private bool HashEquals(byte[] a, byte[] b)
	{
		return a.SequenceEqual(b);
	}
}

public interface ISyncFileProvider
{
	IEnumerable<SyncFileInfo> GetFiles();
	IEnumerable<SyncBlockMetaInfo> GetFileBlocks(string relativePath, int blockSize);
	SyncBlockData ReadBlock(string relativePath, long offset, int len);
	void WriteBlock(string relativePath, byte[] data, long offset);
	void SetFileLength(string relativePath, long len);
	void RemoveFile(string relativePath);
}

public class LocalFSFileProvider : ISyncFileProvider
{
	private string _root;
	private IHashFunction _hash = new SHA256Hash();
	
	public LocalFSFileProvider(string root)
	{
		_root = root;
	}

	public IEnumerable<SyncFileInfo> GetFiles()
	{
		var dirInfo = new DirectoryInfo(_root);
		
		List<SyncFileInfo> files = new List<SyncFileInfo>();
		
		foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
		{
			files.Add(new SyncFileInfo()
			{
				RelativePath = GetRelativePath(file.FullName, _root),
			});
		}
		
		return files;
	}

	public IEnumerable<SyncBlockMetaInfo> GetFileBlocks(string relativePath, int blockSize)
	{
		string fullPath = Path.Combine(_root, relativePath);
		
		Util.Metatext($"Composing file blocks for '{fullPath}' with block size {blockSize}").Dump();
		
		long offset = 0;
		
		List<SyncBlockMetaInfo> blocks = new List<UserQuery.SyncBlockMetaInfo>();
		
		using (Stream stream = File.OpenRead(fullPath))
		{
			int readBytes;
			
			do
			{
				byte[] block = new byte[blockSize];

				stream.Seek(offset, SeekOrigin.Begin);

				readBytes = stream.Read(block, 0, blockSize);

				blocks.Add(new SyncBlockMetaInfo()
				{
					Hash = _hash.Compute(block),
					Offset = offset,
					Length = readBytes,
				});
				
				offset += blockSize;
			}
			while (readBytes == blockSize);
		}
		
		return blocks;
	}

	public SyncBlockData ReadBlock(string relativePath, long offset, int len)
	{
		string fullPath = Path.Combine(_root, relativePath);

		Util.Metatext($"Reading block for {fullPath}, offset: {offset}, len: {len}").Dump();

		using (Stream stream = File.OpenRead(fullPath))
		{
			stream.Seek(offset, SeekOrigin.Begin);
			
			byte[] data = new byte[len];
			
			stream.Read(data, 0, len);
			
			return new SyncBlockData()
			{
				Data = data,
			};
		}
	}

	public void WriteBlock(string relativePath, byte[] data, long offset)
	{
		string fullPath = Path.Combine(_root, relativePath);
		
		Util.Metatext($"Writing block for {fullPath}, offset: {offset}, len: {data.Length}").Dump();

		using (Stream stream = File.Open(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
		{
			stream.Seek(offset, SeekOrigin.Begin);
			stream.Write(data, 0, data.Length);
		}
	}

	public void SetFileLength(string relativePath, long len)
	{
		string fullPath = Path.Combine(_root, relativePath);

		using (Stream stream = File.Open(fullPath, FileMode.Open, FileAccess.Write))
		{
			stream.SetLength(len);
		}
	}

	private string GetRelativePath(string fullPath, string dir)
	{
		if (!fullPath.StartsWith(dir))
			throw new InvalidOperationException();
			
		return fullPath.Substring(dir.Length + 1);
	}

	public void RemoveFile(string relativePath)
	{
		string fullPath = Path.Combine(_root, relativePath);
		
		Util.Metatext($"Removing file {fullPath}").Dump();
		
		File.Delete(fullPath);
	}
}

public interface IHashFunction
{
	byte[] Compute(byte[] data);
}

public class SHA256Hash : IHashFunction
{
	public byte[] Compute(byte[] data)
	{
		using (var sha256 = SHA256.Create())
		{
			return sha256.ComputeHash(data);
		}
	}
}