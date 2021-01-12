namespace HelloWorld.semantic
{
  //using Microsoft.ProgramSynthesis.DslLibrary;

  public static class Semantics
  {
    public static string Substring(string sr, int p1, int p2)
    {
      if(p1 < 0 || sr.Length < p1 || p2 < 0 || sr.Length < p2 || sr.Length < p1+p2)
      {
        return null;
      }
      else
      {
        return sr.Substring(p1, p2);
      }
    }
  }
}
