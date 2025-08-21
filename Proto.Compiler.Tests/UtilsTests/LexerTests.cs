namespace Proto.Compiler.Tests.UtilsTests;

using Proto.Compiler.Lexer;

[TestFixture]
public class LexerTests {
  [TestCase("")]
  [TestCase("   ")]
  [TestCase("\t\n\r\n")]
  public void Lexer_ShouldHandleEmptyInput(string input) {
    var tokens = GetTokens(input);
    Assert.That(tokens.Count, Is.EqualTo(0));
  }

  private static List<Token> GetTokens(string input) {
    var reader = new StringReader(input);
    var lexer = new Lexer(reader);
    return lexer.ToList();
  }
}
