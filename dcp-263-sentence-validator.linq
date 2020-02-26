<Query Kind="Program" />

// This problem was asked by Nest.
// 
// Create a basic sentence checker that takes in a stream of characters and determines whether they form valid sentences. If a sentence is valid, the program should print it out.
// 
// We can consider a sentence valid if it conforms to the following rules:
// 
// The sentence must start with a capital letter, followed by a lowercase letter or a space.
// All other characters must be lowercase letters, separators (',', ';', ':') or terminal marks ('.', '?', '!' , '‽').
// There must be a single space between each word.
// The sentence must end with a terminal mark immediately following a word.

void Main()
{
	var validator = new SentenceValidator();
	
	validator.IsValid("Is this valid?").Dump("yes");
	validator.IsValid("This is valid!").Dump("yes");
	validator.IsValid("This should be valid.").Dump("yes");
	validator.IsValid("I think, this is valid!").Dump("yes");
	validator.IsValid("Is  this valid!").Dump("no");
	validator.IsValid("is this valid!").Dump("no");
	validator.IsValid("Is this valid").Dump("no");
	validator.IsValid("Is this valid?!").Dump("no");
}

public class SentenceValidator
{
	private enum State
	{
		Start,
		Word,
		Space,
		Separator,
		End
	}
	
	private enum Lexem
	{
		Unknown,
		CapitalLetter,
		LowerCaseLetter,
		Space,
		Separator,
		TermialLetter,
	}
	
	private Dictionary<State, (Lexem Lexem, State NextState)[]> _m;
	
	public SentenceValidator()
	{
		_m = new Dictionary<State, (Lexem Lexem, State NextState)[]>();
		
		_m[State.Start] = new [] { (Lexem.CapitalLetter, State.Word), };
		_m[State.Word] = new[] { (Lexem.LowerCaseLetter, State.Word), (Lexem.Space, State.Space), (Lexem.Separator, State.Separator), (Lexem.TermialLetter, State.End) };
		_m[State.Space] = new[] { (Lexem.LowerCaseLetter, State.Word) };
		_m[State.Separator] = new[] { (Lexem.Space, State.Space) };
		_m[State.End] = new (Lexem Lexem, State NextState)[] { };
	}
	
	public bool IsValid(string sentence)
	{
		State state = State.Start;
		
		foreach (Lexem lexem in Parse(sentence))
		{
			//Util.Metatext($" processing {lexem} from state {state}").Dump();
			
			if (!_m[state].Any(x => x.Lexem == lexem))
				return false;
				
			State next = _m[state].First(x => x.Lexem == lexem).NextState;
			
			//Util.Metatext($"   go to {next}").Dump();
			
			state = next;
		}
		
		return state == State.End;
	}
	
	private IEnumerable<Lexem> Parse(string sentence)
	{
		foreach (var c in sentence)
		{
			if (Char.IsUpper(c))
				yield return Lexem.CapitalLetter;
			else if (Char.IsLower(c))
				yield return Lexem.LowerCaseLetter;
			else if (c == ' ')
				yield return Lexem.Space;
			else if (c == ',' || c == ';' || c == ':')
				yield return Lexem.Separator;
			else if (c == '.' || c == '!' || c == '?')
				yield return Lexem.TermialLetter;
			else
				yield return Lexem.Unknown;
		}
	}
}
