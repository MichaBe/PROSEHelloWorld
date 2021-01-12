namespace HelloWorld.syntax
{
  using System.IO;
  using System.Reflection;

  public static class GrammarText
  {
    public static string Get()
    {
      var assembly = typeof(GrammarText).GetTypeInfo().Assembly;
      using (var stream = assembly.GetManifestResourceStream("HelloWorld.syntax.HelloWorld.syntax.grammar"))
      {
        using (var reader = new StreamReader(stream))
        {
          return reader.ReadToEnd();
        }
      }
    }
  }
}
