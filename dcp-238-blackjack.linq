<Query Kind="Program" />

// This problem was asked by MIT.
// 
// Blackjack is a two player card game whose rules are as follows:
// 
// The player and then the dealer are each given two cards.
// The player can then "hit", or ask for arbitrarily many additional cards,
// so long as their total does not exceed 21.
// The dealer must then hit if their total is 16 or lower, otherwise pass.
// Finally, the two compare totals, and the one with the greatest sum not
// exceeding 21 is the winner.
// For this problem, cards values are counted as follows: each card between
// 2 and 10 counts as their face value, face cards count as 10, and aces count as 1.
// 
// Given perfect knowledge of the sequence of cards in the deck, implement
// a blackjack solver that maximizes the player's score (that is, wins minus losses).

void Main()
{
	Score(dealer: new[] { 10, 10 }, player: new[] { 8, 8 }, new[] { 5 }).Dump("1"); // hit
	Score(dealer: new[] { 10, 10 }, player: new[] { 8, 8 }, new[] { 10 }).Dump("-1"); // does not matter, player looses
	Score(dealer: new[] { 5, 5 }, player: new[] { 5, 5 }, new[] { 7, 6, 5, 5 }).Dump("1"); // hit hit skip
	Score(dealer: new[] { 5, 5 }, player: new[] { 5, 5 }, new[] { 6, 6, 5, 5 }).Dump("1"); // hit hit skip
	Score(dealer: new[] { 5, 5 }, player: new[] { 5, 5 }, new[] { 5, 6, 5, 5 }).Dump("-1"); // hit hit skip, dealer gets 21
	Score(dealer: new[] { 5, 5 }, player: new[] { 5, 5 }, new[] { 5, 6, 1, 5, 5 }).Dump("1"); // hit skip, dealer gets 22 and loses
	Score(dealer: new[] { 5, 5 }, player: new[] { 5, 5 }, new[] { 4, 2, 3, 10, 1, 2, 4, 2, 5, 7, 10, 2, 4, 5, 7, 10, 10, 1, 1, 2}).Dump("3"); // I can not verify the answer, but it has to be positive
}

public class State
{
	public List<int> Dealer { get; set; }
	public List<int> Player { get; set; }
	public Stack<int> Deck { get; set; }
	
	public State(IEnumerable<int> dealer, IEnumerable<int> player, IEnumerable<int> deck)
	{
		Dealer = new List<int>(dealer);
		Player = new List<int>(player);
		Deck = new Stack<int>(deck);
	}
	
	public State Clone()
	{
		return new State(Dealer, Player, Deck.Reverse());
	}
}


public class Estimate
{
	public int Value { get; set; }
	public IEnumerable<string> Steps { get; set; }
	
	public Estimate(int value, params string[] steps)
	{
		Value = value;
		Steps = steps?.ToList();
	}
	
	public Estimate AddStep(string step)
	{
		return new Estimate(Value, Steps.Append(step).ToArray());
	}
}

int Score(IEnumerable<int> dealer, IEnumerable<int> player, IEnumerable<int> deck)
{
	var estmate = Score(new State(dealer, player, deck));
	
	estmate.Steps.Reverse().Dump();
	
	return estmate.Value;
}

Estimate Score(State state)
{
	// base case
	if (state.Deck.Count == 0)
		return new Estimate(value: HandScore(state.Player, against: state.Dealer), "deck is over");
		
	// okay, there are some cards there, player can either get one, or skip
	
	State skipBranch = state.Clone();
	
	// skip case: dealer takes cards while there are 16 or less and we compare
	while (skipBranch.Dealer.Sum() <= 16 && skipBranch.Deck.Count > 0)
		skipBranch.Dealer.Add(skipBranch.Deck.Pop());
		
	int currentRoundSkipScore = HandScore(skipBranch.Player, skipBranch.Dealer);

	Estimate skipEstimate;

	// new round for the skip branch
	if (skipBranch.Deck.Count >= 4)
	{
		skipBranch.Dealer.Clear();
		skipBranch.Player.Clear();

		skipBranch.Dealer.Add(skipBranch.Deck.Pop());
		skipBranch.Dealer.Add(skipBranch.Deck.Pop());

		skipBranch.Player.Add(skipBranch.Deck.Pop());
		skipBranch.Player.Add(skipBranch.Deck.Pop());
		
		// go deeper!
		skipEstimate = Score(skipBranch);

		// add current round
		skipEstimate.Value += currentRoundSkipScore;
	} else
	{
		skipEstimate = new Estimate(currentRoundSkipScore, "game over");
	}

	State hitBranch = state.Clone();

	// take a card!
	hitBranch.Player.Add(hitBranch.Deck.Pop());

	Estimate hitEstimate = Score(hitBranch);
	
	if (hitEstimate.Value > skipEstimate.Value)
		return hitEstimate.AddStep("hit");
	else
		return skipEstimate.AddStep("skip");
}

int HandScore(IEnumerable<int> hand, IEnumerable<int> against)
{
	if (hand.Sum() > 21 && against.Sum() > 21)
		return 0; // draw
		
	if (hand.Sum() > 21)
		return -1; // loose
		
	if (against.Sum() > 21)
		return 1; // win
		
	if (hand.Sum() == against.Sum())
		return 0; // draw
		
	return hand.Sum() > against.Sum() ? 1 : -1;
}
