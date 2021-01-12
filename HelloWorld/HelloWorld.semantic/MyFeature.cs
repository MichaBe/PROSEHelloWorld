namespace HelloWorld.semantic
{
  using Microsoft.ProgramSynthesis;
  using Microsoft.ProgramSynthesis.AST;
  using Microsoft.ProgramSynthesis.Features;

  public class MyFeature : Feature<double>
  {
    public MyFeature(Grammar grammar) : base(grammar, "Score", isComplete: true) { }

    protected override double GetFeatureValueForVariable(VariableNode variable) => 0;
    
    [FeatureCalculator(nameof(Semantics.Substring))]
    public static double Score_Substring(double myInputString, double posA, double posB)
    {
      return 1.0 / ((posA * posB) + 1.0);
    }

    [FeatureCalculator("posA", Method = CalculationMethod.FromLiteral)]
    public static double Score_posA(uint posA) { return posA; }

    [FeatureCalculator("posB", Method = CalculationMethod.FromLiteral)]
    public static double Score_posB(uint posB) { return posB; }
  }
}
