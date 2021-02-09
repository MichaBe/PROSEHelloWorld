namespace Annagrammer
{
  using Annagrammer.semantic;
  using Microsoft.ProgramSynthesis.Specifications.Extensions;
  using System;
  using Utilities;

  class Program
  {
    static void Main(string[] args)
    {
      var grammar = Helper.LoadGrammar("Annagrammer.syntax.grammar", true, typeof(StringOfLetters), typeof(Semantics));

      if (grammar != null)
      {
        var specification = ShouldConvert.Given(grammar).To(new StringOfLetters("Hallo"), new StringOfLetters("aHllo"));

        var result = Helper.LearnDeductive(grammar, specification, "learning.log.xml");

        if(result.RealizedPrograms != null)
        {
          Console.WriteLine("No program found!");
        }
        else
        {
          // TODO for ranking, define a feature
          // for now, iterate over the programms without score or other features.
          int i = 0;
          foreach(var program in result.RealizedPrograms)
          {
            Console.WriteLine($"[{i}]: {program}");
            i++;
          }
          // TODO programm auf neuen Input anwenden.
        }
      }
    }
  }
}
