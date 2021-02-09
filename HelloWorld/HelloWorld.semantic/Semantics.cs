namespace HelloWorld.semantic
{
  //using Microsoft.ProgramSynthesis.DslLibrary;
  using System.Text.RegularExpressions;

  public static class Semantics
  {
    public static string Substring(string sr, int? start, int length)
    {
      if(start == null || start < 0 || sr.Length < start || length < 0 || sr.Length < length || sr.Length < start+length)
      {
        return null;
      }
      else
      {
        return sr.Substring((int)start, length);
      }
    }

    public static int? RegexFunction(int i, Regex regex, string s)
    {
      var m = regex.Matches(s);
      return m.Count != 0 && i < m.Count ? m[i].Index : (int?)null;
    }
  }
}
