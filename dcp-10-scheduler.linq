<Query Kind="Program" />

//This problem was asked by Apple.
//
//Implement a job scheduler which takes in a function f and an integer n, and calls f after n milliseconds.


void Main()
{
	var scheduler = new JobScheduler();
	scheduler.Start();
	
	scheduler.AddJob(new Job(() => "job".Dump(), DateTime.UtcNow.AddSeconds(1)));
	scheduler.AddJob(new Job(() => "job2".Dump(), DateTime.UtcNow.AddSeconds(2)));
	scheduler.AddJob(new Job(() => "job3".Dump(), DateTime.UtcNow.AddSeconds(3)));
	
	bool shutDownCompleted = scheduler.Shutdown(TimeSpan.FromSeconds(5), waitQueuedJobs: true);
	
	shutDownCompleted.Dump("completed shutdown");
}

public class Job
{
	private Action _f;
	public DateTime NotBefore { get; }
	
	public Job(Action f, DateTime notBefore)
	{
		_f = f;
		
		NotBefore = notBefore;
	}
	
	public void Do()
	{
		_f.Invoke();
	}
}

public class JobScheduler
{
	private List<Job> _jobs = new List<UserQuery.Job>();
	
	private Thread _thread;
	private int _started;
	private bool _shutdown;
	private bool _waitQueuedJobs;
	private bool _stopped;
	
	public void Start()
	{
		if (Interlocked.CompareExchange(ref _started, 1, 0) == 0)
		{
			_thread = new Thread(WorkerWork);
			_thread.Start();
		}
	}
	
	public bool Shutdown(TimeSpan timeout, bool waitQueuedJobs)
	{
		_waitQueuedJobs = waitQueuedJobs;
		
		Interlocked.MemoryBarrier();
		
		_shutdown = true;
		
		bool exited = SpinWait.SpinUntil (() => _stopped, timeout);
		
		return exited;
	}
	
	public void AddJob(Job job)
	{
		if (_shutdown || _stopped)
			throw new InvalidOperationException();
		
		_jobs.Add(job);
	}
	
	private void WorkerWork()
	{
		while (true)
		{
			if (_shutdown)
			{
				if (_waitQueuedJobs)
				{
					if (!_jobs.Any())
						break;
				} else
				{
					break;
				}
			}
			
			foreach (Job job in _jobs.ToArray())
			{
				if (job.NotBefore.ToUniversalTime() <= DateTime.UtcNow)
				{
					job.Do();
					_jobs.Remove(job);
				}
			}
			
			Thread.Sleep(10);
		}
		
		_stopped = true;
	}
}
