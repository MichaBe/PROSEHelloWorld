namespace JustSomeSimpleTests
{
  using System;
  using System.Text.RegularExpressions;
  using HelloWorld.semantic;

  class Program
  {
    static void Main()
    {
      var result = Semantics.Substring("Hello World", Semantics.RegexFunction(0, SimpleLearner.regexList[0], "Hello World"), 2);
      var myRegex = new Regex(@"[A-Z]", RegexOptions.Compiled);
      var matches = myRegex.Matches("dies ist ein test, Wie viele grossbuchstaben vorkommen...");
      Console.WriteLine();
    }
  }
}
