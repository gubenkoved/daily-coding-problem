<Query Kind="Program" />

// This problem was asked by Google.
// 
// Suppose we represent our file system by a string in the following manner:
// 
// The string "dir\n\tsubdir1\n\tsubdir2\n\t\tfile.ext" represents:
// 
// dir
//     subdir1
//     subdir2
//         file.ext
// The directory dir contains an empty sub-directory subdir1 and a sub-directory subdir2 containing a file file.ext.
// 
// The string "dir\n\tsubdir1\n\t\tfile1.ext\n\t\tsubsubdir1\n\tsubdir2\n\t\tsubsubdir2\n\t\t\tfile2.ext" represents:
// 
// dir
//     subdir1
//         file1.ext
//         subsubdir1
//     subdir2
//         subsubdir2
//             file2.ext
// The directory dir contains two sub-directories subdir1 and subdir2. subdir1 contains a file file1.ext
// and an empty second-level sub-directory subsubdir1. subdir2 contains a second-level sub-directory
// subsubdir2 containing a file file2.ext.
// 
// We are interested in finding the longest (number of characters) absolute path to a file within our file system.
// For example, in the second example above, the longest absolute path is
// "dir/subdir2/subsubdir2/file2.ext", and its length is 32 (not including the double quotes).
// 
// Given a string representing the file system in the above format, return the length of the longest absolute
// path to a file in the abstracted file system. If there is no file in the system, return 0.
// 
// Note:
// 
// The name of a file contains at least a period and an extension.
// 
// The name of a directory or sub-directory will not contain a period.

void Main()
{
	string fs0 =
@"dir
dir1
dir2
file.ext";

	string fs1 = 
@"dir
	subdir1
	subdir2
		file.ext";

	string fs2 =
@"dir
	subdir1
		file1.ext
		subsubdir1
			file3.ext
	subdir2
		subsubdir2
		file2.ext";

	LongestFilePath(fs0).Dump("file.ext");
	LongestFilePath(fs1).Dump("dir/subdir2/file.ext");
	LongestFilePath(fs2).Dump("dir/subdir1/subsubdir1/file3.ext");
}

public string LongestFilePath(string s)
{
	// just for convinience, go UNIX style
	s = s.Replace("\r\n", "\n");
	
	// just go and maintain stack of current directories
	// when we encounter a file concat the path with all the current dirs and see it is longer than current longest
	
	Func<string, bool> isFile = x => x.IndexOf('.') >= 0;
	
	string longest = string.Empty;
	string currentName = string.Empty;
	
	Stack<string> dirs = new Stack<string>();
	int idx = 0;
	
	while (true)
	{
		char? c = null;
		
		if (idx < s.Length)
			c = s[idx];

		bool endOfToken = c == '\n' || c == null; // either new line or end of text

		if (!endOfToken)
		{
			currentName += c;
		} else
		{
			if (!isFile(currentName))
			{
				// we got a directory -- add to current
				dirs.Push(currentName);
				
				Util.Metatext($"cwd: {string.Join("/", dirs.Reverse())}").Dump();
			} else
			{
				// looks like we got a file, maintain longest

				string fullPath = string.Join("/", dirs.Reverse().Union(new[] { currentName }));

				if (fullPath.Length > longest.Length)
					longest = fullPath;

				Util.Metatext($"file: {fullPath}").Dump();
			}
			
			if (c == null) // eof
				break;
			
			// going next level
			// we will need to read how many tab chars we have and change current dir path accordingly
			// if amount of tabs (T) is equal to amount of current dir then we go to subdirectory
			// otherwise pop dirs until we left with exactly T items in stack
			int level = 0;
			
			// read all tabs
			while (s[idx + 1] == '\t')
			{
				level += 1;
				idx += 1;
			}
			
			while (level < dirs.Count)
				dirs.Pop();
				
			currentName = string.Empty;
		}
		
		// go next char
		idx += 1;
	}
	
	return longest;
}