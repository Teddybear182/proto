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

  [TestCase("12")]
  [TestCase("0")]
  [TestCase("5768485")]
  public void Lexer_ShouldHandleIntegers(string input) {
    var token = GetToken(input);
    Assert.Multiple(() => {
      Assert.That(token.Type, Is.EqualTo(TokenType.IntegerLiteral));
      Assert.That(token.Value, Is.EqualTo(input));
    });
  }

  [TestCase("12.5")]
  [TestCase("0.001")]
  [TestCase("5.")]
  public void Lexer_ShouldHandleFloats(string input) {
    var token = GetToken(input);
    Assert.Multiple(() => {
      Assert.That(token.Type, Is.EqualTo(TokenType.FloatLiteral));
      Assert.That(token.Value, Is.EqualTo(input));
    });
  }

  [TestCase("break")]
  [TestCase("continue")]
  [TestCase("return")]
  [TestCase("var")]
  [TestCase("const")]
  [TestCase("begin")]
  [TestCase("end")]
  [TestCase("if")]
  [TestCase("then")]
  [TestCase("else")]
  [TestCase("task")]
  [TestCase("while")]
  public void Lexer_ShouldHandleKeywords(string input) {
    var token = GetToken(input);
    Assert.Multiple(() => {
      Assert.That(token.Type, Is.EqualTo(TokenType.Keyword));
      Assert.That(token.Value, Is.EqualTo(input));
    });
  }

  [TestCase("and")]
  [TestCase("or")]
  [TestCase("not")]
  [TestCase("mod")]
  public void Lexer_ShouldHandleWordOperators(string input) {
    var token = GetToken(input);
    Assert.Multiple(() => {
      Assert.That(token.Type, Is.EqualTo(TokenType.Operator));
      Assert.That(token.Value, Is.EqualTo(input));
    });
  }

  [TestCase("integer")]
  [TestCase("float")]
  [TestCase("string")]
  [TestCase("char")]
  [TestCase("boolean")]
  public void Lexer_ShouldHandleTypeLiterals(string input) {
    var token = GetToken(input);
    Assert.Multiple(() => {
      Assert.That(token.Type, Is.EqualTo(TokenType.TypeLiteral));
      Assert.That(token.Value, Is.EqualTo(input));
    });
  }

  [TestCase("true")]
  [TestCase("false")]
  public void Lexer_ShouldHandleBooleanLiterals(string input) {
    var token = GetToken(input);
    Assert.Multiple(() => {
      Assert.That(token.Type, Is.EqualTo(TokenType.BooleanLiteral));
      Assert.That(token.Value, Is.EqualTo(input));
    });
  }

  #region Helper Methods

  private static List<Token> GetTokens(string input) {
    var reader = new StringReader(input);
    var lexer = new Lexer(reader);
    return lexer.ToList();
  }

  private static Token GetToken(string input) {
    var tokens = GetTokens(input);
    return tokens[0];
  }

  #endregion
}
