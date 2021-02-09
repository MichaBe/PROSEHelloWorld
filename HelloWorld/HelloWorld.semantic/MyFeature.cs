namespace HelloWorld.semantic
{
  using Microsoft.ProgramSynthesis;
  using Microsoft.ProgramSynthesis.AST;
  using Microsoft.ProgramSynthesis.Features;
  using System.Text.RegularExpressions;

  public class MyFeature : Feature<double>
  {
    public MyFeature(Grammar grammar) : base(grammar, "MyScore", isComplete: true) { }

    // Feature for myInputString
    protected override double GetFeatureValueForVariable(VariableNode variable) => 1;
    
    [FeatureCalculator(nameof(Semantics.Substring), Method = CalculationMethod.FromChildrenFeatureValues)]
    public static double Score_Substring(double myInputStringFV, double startPosFV, double lengthFV)
    {
      return myInputStringFV*startPosFV*lengthFV;
    }

    [FeatureCalculator(nameof(Semantics.RegexFunction), Method = CalculationMethod.FromChildrenFeatureValues)]
    public static double Score_RegexFunction(double iTHFV, double rFV, double myInputStringFV)
    {
      return iTHFV * rFV * myInputStringFV;
    }

    [FeatureCalculator("AbsPos", Method=CalculationMethod.FromChildrenFeatureValues)]
    public static double Score_AbsPos(int sFV, int posFV) => sFV * posFV;

    [FeatureCalculator("posA", Method = CalculationMethod.FromLiteral)]
    public static double Score_posA(int posA) => 0.5;

    [FeatureCalculator("length", Method = CalculationMethod.FromLiteral)]
    public static double Score_posB(int length) => 1.0;

    [FeatureCalculator("r", Method = CalculationMethod.FromLiteral)]
    public static double Score_r(Regex r)
    {
      return r.Options.HasFlag(RegexOptions.Compiled) ? 1.0 : 0.2;
    }

    [FeatureCalculator("iTH", Method = CalculationMethod.FromLiteral)]
    public static double Score_iTH(int iTH) => 1.0;
  }
}
