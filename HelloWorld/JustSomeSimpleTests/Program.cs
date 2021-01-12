using Microsoft.ProgramSynthesis.DslLibrary;
using System;

namespace JustSomeSimpleTests
{
  class Program
  {
    static void Main()
    {
      StringRegion sr = new StringRegion("Dies ist ein Test",Token.Tokens);
      var sl = sr.Slice(6, 8);
      Console.WriteLine("\""+sl.ToString()+"\"");
    }
  }
}
