namespace HelloWorld
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Reflection;
  using Microsoft.ProgramSynthesis;
  using Microsoft.ProgramSynthesis.AST;
  using Microsoft.ProgramSynthesis.Compiler;
  using Microsoft.ProgramSynthesis.Learning;
  using Microsoft.ProgramSynthesis.Learning.Logging;
  using Microsoft.ProgramSynthesis.Learning.Strategies;
  using Microsoft.ProgramSynthesis.Specifications.Extensions;

  class Program
  {
    static void Main()
    {
      var compilationResult = DSLCompiler.Compile(new CompilerOptions()
      {
        InputGrammarText = File.ReadAllText("HelloWorld.syntax.grammar"),
        References = CompilerReference.FromAssemblyFiles(
          typeof(HelloWorld.semantic.Semantics).GetTypeInfo().Assembly
        ),
      });

      if (compilationResult.HasErrors)
      {
        foreach (var error in compilationResult.Diagnostics)
        {
          Console.WriteLine($"[{error.Location}]: {error.Message}");
        }
        return;
      }

      var grammar = compilationResult.Value;
      
      var program = LearnProgram(grammar);

      if(program != null)
      {
        Console.WriteLine("Execute the first realized Program on some more input:");
        var input = "Hallo Welt";
        State s = State.CreateForExecution(program.Grammar.InputSymbol, input);
        var output = (string)program.Invoke(s);
        Console.WriteLine($"{input} -> {output}");
      }
      else
      {
        Console.WriteLine("No programm found");
      }
    }

    private static ProgramNode LearnProgram(Grammar grammar)
    {
      var specification = ShouldConvert.Given(grammar).To("Hello World", "Wo");

      var engine = new SynthesisEngine(grammar, new SynthesisEngine.Config
      {
        Strategies = new ISynthesisStrategy[] {
          new EnumerativeSynthesis(new EnumerativeSynthesis.Config{MaximalSize = 100}),
          //new DeductiveSynthesis(null)
        },
        UseThreads = true,
        LogListener = new LogListener(),
      });

      var consistentPrograms = engine.LearnGrammar(specification);
      engine.Configuration.LogListener.SaveLogToXML("learning.log.xml");

      Console.WriteLine("Found the following programms:");
      foreach (var p in consistentPrograms.RealizedPrograms)
      {
        Console.WriteLine(p);
      }

      return consistentPrograms.RealizedPrograms.Count() == 0 ? null : consistentPrograms.RealizedPrograms.First();
    }
  }
}
