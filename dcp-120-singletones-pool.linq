<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Implement the singleton pattern with a twist. First,
// instead of storing one instance, store two instances.
// And in every even call of getInstance(), return the
// first instance and in every odd call of getInstance(),
// return the second instance.

void Main()
{
	var pool = new Pool<TestObj>(() => new TestObj(), 2);
	
	for (int i = 0; i < 10; i++)
	{
		var instance = pool.GetInstance();
		
		instance.Dump();
	}
}

public class TestObj
{
	private Guid _instanceId;

	public TestObj()
	{
		_instanceId = Guid.NewGuid();
		
		$"constructed {_instanceId}".Dump();
	}

	public override string ToString()
	{
		return $"Instance {_instanceId}";
	}
}

public class Pool<T>
{
	private Func<T> _gen;
	
	private int _counter = -1;
	private bool[] _initialized;
	private T[] _instances;
	private object _syncRoot = new object();
	
	public Pool(Func<T> gen, int capacity)
	{
		_gen = gen;
		_instances = new T[capacity];
		_initialized = new bool[capacity];
	}
	
	public T GetInstance()
	{
		int index = Interlocked.Increment(ref _counter);
		
		// trying to keep counter below the instance count in a thread safe maner
		if (index >= _instances.Length)
			Interlocked.CompareExchange(ref _counter, index % _instances.Length, index);
		
		//_counter.Dump();
		
		int instanceIndex = index % _instances.Length;
		
		// initialize at index!
		if (!_initialized[instanceIndex])
		{
			lock (_syncRoot)
			{
				// double checking locking
				if (!_initialized[instanceIndex])
				{
					_instances[instanceIndex] = _gen();
					_initialized[instanceIndex] = true;
				}
			}
		}
		
		return _instances[instanceIndex];
	}
}