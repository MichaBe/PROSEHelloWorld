namespace Annagrammer.semantic
{

  public static class Semantics
  {
    public static StringOfLetters Swapper(StringOfLetters input, int pos)
    {
      if(input == null || pos+1 >= input.letters.Length)
      {
        return null;
      }
      else
      {
        var iTemp = input.letters[pos];
        input.letters[pos] = input.letters[pos + 1];
        input.letters[pos + 1] = iTemp;
        return input;
      }
    }
  }
}
