namespace HelloWorld
{
  using System;
  using System.Linq;
  using HelloWorld.semantic;
  using Microsoft.ProgramSynthesis.Specifications.Extensions;
  using Utilities;

  class Program
  {
    static void Main()
    {
      var grammar = Helper.LoadGrammar("HelloWorld.syntax.grammar", true, typeof(HelloWorld.semantic.Semantics), typeof(System.Text.RegularExpressions.Regex));

      var Spec = ShouldConvert.Given(grammar).To("Hello World", "He").To("hello World", "Wo");
      
      var program = Helper.LearnDeductively(grammar, Spec, new WitnessFunctions(grammar), "learning_ded.log.xml");

      if(program.IsEmpty)
      {
        Console.WriteLine("Could not learn any program.");
        return;
      }

      /*var programs = Helper.Learn(grammar, ShouldConvert.Given(grammar).To("Hello World", "Wo"), "learning.log.xml"); //LearnProgram(grammar);
      var bestPrograms = Helper.RankPrograms(programs, 10, new MyFeature(grammar));

      var scorer = new MyFeature(grammar);
      var funcCounter = new FunctionCounter(grammar);
      foreach (var p in bestPrograms)
      {
        Console.WriteLine($"[Score: {p.GetFeatureValue(scorer):F3}, Function calls: {p.GetFeatureValue(funcCounter)}] {p}");
      }

      var bestProgram = bestPrograms.First();

      if (bestProgram != null)
      {
        Console.WriteLine("Execute the first realized Program on some more input:");
        var input = "Dies ist ein Test";
        var output = (string)Helper.ExecuteOn(bestProgram, input);
        Console.WriteLine($"{input} -> {output}");
      }
      else
      {
        Console.WriteLine("No programm found");
      }*/
    }
  }
}
