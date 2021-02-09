namespace HelloWorld.semantic
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;

  public static class SimpleLearner
  {
    /*public static IEnumerable<int> iGen100 = Enumerable.Range(0, 101);
    public static IEnumerable<int> iGen10 = Enumerable.Range(0, 11);*/

    public static Regex[] regexList = new[]
    {
      new Regex(@"\b[A-Z]", RegexOptions.Compiled),
      new Regex(@"[A-Z]"),
    };
  }
}
