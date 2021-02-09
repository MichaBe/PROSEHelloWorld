namespace Annagrammer.semantic
{
  public class StringOfLetters
  {
    public char[] letters;

    public StringOfLetters(string s)
    {
      //letters = s.ToCharArray();
      letters = new char[s.Length];
      for(int i = 0; i < s.Length; ++i)
      {
        letters[i] = s[i];
      }
    }

    public override string ToString()
    {
      return new string(letters);
    }

    public static bool operator ==(StringOfLetters lhs, StringOfLetters rhs)
    {
      if (rhs is null && lhs is null)
        return true;
      if(rhs is null || lhs is null)
        return false;
      bool returner = lhs.letters.Length == rhs.letters.Length;
      for(int i = 0; i < lhs.letters.Length && returner; ++i)
      {
        returner = lhs.letters[i] == rhs.letters[i];
      }
      return returner;
    }
    public static bool operator !=(StringOfLetters lhs, StringOfLetters rhs)
    {
      if (rhs is null && lhs is null)
        return false;
      if(rhs is null || lhs is null)
        return true;
      return !(lhs == rhs);
    }
  }
}
