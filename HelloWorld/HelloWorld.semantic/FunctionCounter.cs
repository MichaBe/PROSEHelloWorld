namespace HelloWorld.semantic
{
  using Microsoft.ProgramSynthesis;
  using Microsoft.ProgramSynthesis.AST;
  using Microsoft.ProgramSynthesis.Features;
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Text.RegularExpressions;

  public class FunctionCounter : Feature<int>
  {
    public FunctionCounter(Grammar grammar) : base(grammar, "funCount", isComplete: true) { }

    protected override int GetFeatureValueForVariable(VariableNode variable) => 0;

    [FeatureCalculator(nameof(Semantics.Substring), Method = CalculationMethod.FromChildrenFeatureValues)]
    public static int Score_Substring(int myInputStringFV, int startPosFV, int lengthFV) => myInputStringFV + startPosFV + lengthFV + 1;

    [FeatureCalculator(nameof(Semantics.RegexFunction), Method = CalculationMethod.FromChildrenFeatureValues)]
    public static int Score_RegexFunction(int iTHFV, int rFV, int myInputStringFV) => iTHFV + rFV + myInputStringFV + 1;

    [FeatureCalculator("posA", Method = CalculationMethod.FromLiteral)]
    public static int Score_posA(int posA) => 0;

    [FeatureCalculator("length", Method = CalculationMethod.FromLiteral)]
    public static int Score_posB(int length) => 0;

    [FeatureCalculator("r", Method = CalculationMethod.FromLiteral)]
    public static int Score_r(Regex r) => 0;

    [FeatureCalculator("iTH", Method = CalculationMethod.FromLiteral)]
    public static int Score_iTH(int iTH) => 0;

    [FeatureCalculator("AbsPos", Method = CalculationMethod.FromChildrenFeatureValues)]
    public static int Score_AbsPos(int sFV, int posFV) => sFV + posFV + 1;
  }
}
