namespace Utilities
{
  using Microsoft.ProgramSynthesis;
  using Microsoft.ProgramSynthesis.AST;
  using Microsoft.ProgramSynthesis.Compiler;
  using Microsoft.ProgramSynthesis.Features;
  using Microsoft.ProgramSynthesis.Learning;
  using Microsoft.ProgramSynthesis.Learning.Logging;
  using Microsoft.ProgramSynthesis.Learning.Strategies;
  using Microsoft.ProgramSynthesis.Specifications;
  using Microsoft.ProgramSynthesis.VersionSpace;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Reflection;

  public class Helper
  {
    public static Grammar LoadGrammar(string pathGrammarFile, bool verbose = false, params Type[] types)
    {
      var assemblies = new Assembly[types.Length];
      for(int i = 0; i < types.Length; ++i)
      {
        assemblies[i] = types[i].GetTypeInfo().Assembly;
      }
      var compilationResult = DSLCompiler.Compile(new CompilerOptions()
      {
        InputGrammarText = File.ReadAllText(pathGrammarFile),
        References = CompilerReference.FromAssemblyFiles(assemblies)
      });

      if(verbose)
      {
        if(compilationResult.HasErrors)
        {
          Console.WriteLine("Compilation HasErrors! Trace: ");
          compilationResult.TraceDiagnostics();
          Console.WriteLine("------------------------------------------");
        }
        else
        {
          Console.WriteLine("No Errors in Compilation. Trace: ");
          compilationResult.TraceDiagnostics();
          Console.WriteLine("------------------------------------------");
        }
      }

      return compilationResult.HasErrors ? null : compilationResult.Value;
    }

    public static ProgramSet Learn(Grammar g, Spec s, string logXmlFilename)
    {
      var engine = new SynthesisEngine(
      grammar: g,
      config: new SynthesisEngine.Config
      {
        Strategies = new ISynthesisStrategy[] 
        {
          new EnumerativeSynthesis(
            new EnumerativeSynthesis.Config 
            {
              MaximalSize = 101*101*11*2 
            }
          ),
          new ComponentBasedSynthesis(null,null,null)
        },
        UseThreads = true,
        LogListener = new LogListener()
      });

      var consistentPrograms = engine.LearnGrammar(s);
      engine.Configuration.LogListener.SaveLogToXML(logXmlFilename);

      return consistentPrograms;
    }

    public static ProgramSet LearnDeductively(Grammar g, Spec s, DomainLearningLogic witness, string logXmlFilename)
    {
      var engine = new SynthesisEngine(
        grammar: g,
        config: new SynthesisEngine.Config
        {
          Strategies = new[] { new DeductiveSynthesis(witness) },
          UseThreads = true,
          LogListener = new LogListener()
        }
      );
      var consistentPrograms = engine.LearnGrammar(s);
      engine.Configuration.LogListener.SaveLogToXML(logXmlFilename);

      return consistentPrograms;
    }

    public static IEnumerable<ProgramNode> RankPrograms(ProgramSet programs, int topK, IFeature feature)
    {
      return programs.TopK(feature, topK);
    }

    public static object ExecuteOn(ProgramNode program, object input)
    {
      var s = State.CreateForExecution(program.Grammar.InputSymbol, input);
      return program.Invoke(s);
    }

  }
}
