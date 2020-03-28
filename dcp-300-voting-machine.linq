<Query Kind="Program" />

// This problem was asked by Uber.
// 
// On election day, a voting machine writes data in the form (voter_id, candidate_id)
// to a text file. Write a program that reads this file as a stream and returns the
// top 3 candidates at any given time. If you find a voter voting more than once,
// report this as fraud.

void Main()
{
	var processor = new VotesProcessor();
	
	Random r = new Random();
	
	for (int i = 0; i < 1000; i++)
	{
		processor.Process(r.Next() % 10_000, r.Next() % 100);
		string.Join(", ", processor.Top(3)).Dump();
	}
}

class VotesProcessor
{
	class CandidateResults
	{
		public int CandidateId { get; set; }
		public int VoteCount { get; set; }
	}
	
	private HashSet<int> _voted = new HashSet<int>();
	private Dictionary<int, LinkedListNode<CandidateResults>> _resultRefs = new Dictionary<int, LinkedListNode<CandidateResults>>();
	private LinkedList<CandidateResults> _results = new LinkedList<CandidateResults>();

	public void Process(int voterId, int candidateId)
	{
		Util.Metatext($"processing vote of {voterId} for {candidateId}").Dump();
		
		if (_voted.Contains(voterId))
		{
			Util.Highlight($"already voted! {voterId}").Dump();
			return;
		}
		
		_voted.Add(voterId);

		if (!_resultRefs.ContainsKey(candidateId))
			_resultRefs[candidateId] = _results.AddLast(new CandidateResults() { CandidateId = candidateId });

		// update score and move it in the list if required
		var candidateNode = _resultRefs[candidateId];
		
		candidateNode.Value.VoteCount += 1;
		
		// highest voted candidates are the first in the list
		while (candidateNode.Previous != null && candidateNode.Value.VoteCount > candidateNode.Previous.Value.VoteCount)
		{
			var moveBefore = candidateNode.Previous;
			
			_results.Remove(candidateNode);
			_results.AddBefore(moveBefore, candidateNode);
		}
	}
	
	public (int candidateId, int votes)[] Top(int n)
	{
		List<(int candidateId, int votes)> top = new List<(int candidateId, int votes)>();
		
		return _results.Take(n).Select(x => (x.CandidateId, x.VoteCount)).ToArray();
	}
}

