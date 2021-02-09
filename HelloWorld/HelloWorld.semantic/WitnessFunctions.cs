namespace HelloWorld.semantic
{
  using Microsoft.ProgramSynthesis;
  using Microsoft.ProgramSynthesis.Specifications;
  using Microsoft.ProgramSynthesis.Learning;
  using Microsoft.ProgramSynthesis.Rules;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;

  public class WitnessFunctions : DomainLearningLogic
  {
    public WitnessFunctions(Grammar grammar) : base(grammar) { }

    // Annotationsparameter:
    // parameterSymbolName(string) / parameterSymbolIndex(int, 0-based): Für welchen Operatorparameter ist diese Witnessfunktion?
    // DependsOnSymbols(string[]): 
    // DependsOnParameters(int[]): hier können weitere Parameter dieses operators per index angegeben werden, von dem dieser Parameter abhängig ist.
    // Incomplete(bool): 
    // Verify(bool): true: es handelt sich um eine overapproximation. Die ausgegebenen programme müssen also separat nochmal verifiziert werden!
    [WitnessFunction(nameof(Semantics.Substring), parameterSymbolName: "length")]
    DisjunctiveExamplesSpec WitnessSubstring_length(GrammarRule rule, ExampleSpec spec)
    {
      var result = new Dictionary<State, IEnumerable<object>>();
      foreach (var example in spec.Examples)
      {
        //Aufbau: example.Key ist der input für diese Funktion
        // example.Value ist der Output der Funktion
        //der wert des length-parameter lässt sich eindeutig über die Länge des outputs bestimmen
        var length = ((string)example.Value).Length;
        var validLengths = new List<object> { length };
        result[example.Key] = validLengths;
      }
      return new DisjunctiveExamplesSpec(result);
    }

    [WitnessFunction(nameof(Semantics.Substring), parameterSymbolName: "startPos", DependsOnParameters = new[] { 0, 2 })]// <- name nach .grammar-Datei erwartet. So richtig!
    DisjunctiveExamplesSpec WitnessSubstring_startPos(GrammarRule rule, ExampleSpec spec, ExampleSpec spec0, ExampleSpec spec2)
    {
      var innerSpec = new Dictionary<State, IEnumerable<object>>();
      foreach (var example in spec.Examples)
      {
        //Get the output of this operator
        var operatorOutput = (string)example.Value;
        var operatorInputs = example.Key;
        var completeString = (string)operatorInputs[rule.Body[0]]; //gets the first inputparameter, here string sr

        //Find all possible start-positions of operatoroutput in completestring
        var possibleStartPositions = new List<int?>();
        for (int i = completeString.IndexOf(operatorOutput); i >= 0; i = completeString.IndexOf(operatorOutput, i + 1))
        {
          possibleStartPositions.Add(i);
        }
        if (possibleStartPositions.Count == 0)
        {
          return null;
        }
        innerSpec[operatorInputs] = possibleStartPositions.Cast<object>();
      }
      return new DisjunctiveExamplesSpec(innerSpec);
    }

    // spec nach parameter "n" ist abhängig von parameter[1]="regex"
    // TODO auf der tutorial-seite ist das zweite parameter vom typ DisjuncitveExampleSpec. Geht das auch so wie hier?
    // TODO wie funktioniert das mit mehreren parameterabhängigkeiten? symbolabhängigkeiten? reihenfolge?
    // TODO schauen, ob diese angabe überhaupt notwendig, da es sich bei "regex" um ein per generator gefülltes parameter handelt
    [WitnessFunction(nameof(Semantics.RegexFunction), parameterSymbolName: "iTH", DependsOnParameters = new[] { 1 })]
    DisjunctiveExamplesSpec WitnessRegexFunction_iTH(GrammarRule rule, ExampleSpec spec, ExampleSpec regexSpec)
    {
      var result = new Dictionary<State, IEnumerable<object>>();
      // Die erste Spec ist die "äußere spec", die die beispiele enthält
      foreach (var example in spec.Examples)
      {
        var functionInput = (string)example.Key[rule.Body[2]]; //TODO funktioniert das so? 0 -> der index zeigt die position in der ganzen grammatik an, und 0 ist der input. oder muss hier eine 2 hin? -> index zeigt die position in der parameterliste an. TESTEN!
        var regex = new Regex("");// (Regex)regexSpec.Examples[example.Key];
        var completeString = "";
        var programOutput = (string)example.Value;// TODO ?

        //TODO match regex to input and write back, which match-indices have a match, where the output starts, too
        var possibleNs = new HashSet<int>();
        var matches = regex.Matches(functionInput);

        var possibleStartPositions = new List<int?>();
        for (int i = functionInput.IndexOf(programOutput); i >= 0; i = completeString.IndexOf(programOutput, i + 1))
        {
          possibleStartPositions.Add(i);
        }


        if (possibleNs.Count == 0) return null;
        result[example.Key] = possibleNs.Cast<object>();
      }
      return new DisjunctiveExamplesSpec(result);
    }

    [WitnessFunction(nameof(Semantics.RegexFunction), parameterSymbolName: "r", Verify = true)]
    DisjunctiveExamplesSpec WitnessRegexFunction_r(GrammarRule rule, ExampleSpec spec)
    {
      var result = new Dictionary<State, IEnumerable<object>>();
      foreach (var example in spec.Examples)
      {
        result[example.Key] = new[]
        {
          new Regex(@"\b[A-Z]", RegexOptions.Compiled),
          new Regex(@"[A-Z]"),
        };
      }
      return new DisjunctiveExamplesSpec(result);
    }
  }
}
