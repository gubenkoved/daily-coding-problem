<Query Kind="Program" />

// This problem was asked by Two Sigma.
// 
// Ghost is a two-person word game where players alternate appending
// letters to a word. The first person who spells out a word, or creates
// a prefix for which there is no possible continuation, loses. Here is
// a sample game:
// 
// Player 1: g
// Player 2: h
// Player 1: o
// Player 2: s
// Player 1: t [loses]
//
// Given a dictionary of words, determine the letters the first player
// should start with, such that with optimal play they cannot lose.
// 
// For example, if the dictionary is ["cat", "calf", "dog", "bear"],
// the only winning start letter would be b.

void Main()
{
	// it looks like there is an error in the description of probem, as play can
	// start from "c" as well:	
	// 1: "c" -> 2: "a" -> 1: "l" -> 2: "f" and 2nd loses

	// the problem is similar to chess/checkers in a sense
	// we should simulate whole game tree where each move tranfers game to the next stage
	// where there are other moves (driven by dictionary of words) or it's a game over if
	// there are no other words

	GetWinningStartLetters(new[] { "a", }).Dump();
	GetWinningStartLetters(new[] { "a", "bb" }).Dump();
	GetWinningStartLetters(new[] { "a", "bb", "ccc", "dddd" }).Dump();
	GetWinningStartLetters(new[] { "cat", "calf", "dog", "bear" }).Dump();
}

IEnumerable<char> GetWinningStartLetters(IEnumerable<string> dictionary)
{
	// construct a tree of words where each path from root to leaf corresponds to a word
	Node root = new Node(default(char));
	
	foreach (string word in dictionary)
		AddWordToTree(root, word);
	
	// okay, now try to  annotate each Node with the winner assuming the optimal play
	// which is given the choice and ability to calculate all the way down each player
	// makes the choice to win (if that's is possible)
	
	foreach (Node start in root.Children)
	{
		if (GetWinner(start, curPlayer: 2) == 1)
			yield return start.Char;
	}
}

// returns winning player number given current position and that next turn if by given player
int GetWinner(Node cur, int curPlayer)
{
	// looks like previous player finished the word!
	if (!cur.Children.Any())
		return curPlayer;
		
	// okay, there are some other moves to make
	foreach (Node child in cur.Children)
	{
		if (GetWinner(child, NextPlayer(curPlayer)) == curPlayer)
			return curPlayer;
	}
	
	// if we get there, looks like there are NO winning moves this player can make, so he/she lose
	return NextPlayer(curPlayer);
}

int NextPlayer(int curPlayer)
{
	return curPlayer == 1 ? 2 : 1;
}

void AddWordToTree(Node root, string word)
{
	Node cur = root;
	
	for (int i = 0; i < word.Length; i++)
	{
		char c = word[i];
		
		Node childNode = cur.Children.SingleOrDefault(n => n.Char == c);

		if (childNode != null)
		{
			cur = childNode;
		}
		else // node is not created yet
		{
			childNode = new Node(c);
			
			cur.Children.Add(childNode);
			
			cur = childNode;
		}
	}
}

public class Node
{
	public char Char { get; set; }
	public List<Node> Children { get; set; }
	
	public Node(char c, List<Node> children = null)
	{
		Char = c;
		Children = children ?? new List<Node>();
	}
}