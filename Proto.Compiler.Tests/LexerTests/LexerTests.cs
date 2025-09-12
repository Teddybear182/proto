namespace Proto.Compiler.Tests.LexerTests;

using Proto.Compiler.Lexer;

file static class LexerTestsUtils {
  public static void AssertTypeAndValue(this Token token, TokenType type, string value) {
    Assert.Multiple(() => {
      Assert.That(token.Type, Is.EqualTo(type));
      Assert.That(token.Value, Is.EqualTo(value));
    });
  }

  public static void AssertWithChunkTable(this IEnumerable<Token> tokens, TokenType type, IEnumerable<string> chunks) {
    var tokenArray = tokens.ToArray();
    var chunkArray = chunks.ToArray();
    Assert.Multiple(() => {
      Assert.That(tokenArray, Has.Length.EqualTo(chunkArray.Length));
      for (var i = 0; i < tokenArray.Length; i++) {
        tokenArray[i].AssertTypeAndValue(type, chunkArray[i]);
      }
    });
  }
}

[TestFixture]
public class LexerTests {
  #region Language Definition Tests

  [TestCase("")]
  [TestCase("   ")]
  [TestCase("\t\n\r\n")]
  public void Lexer_ShouldHandleEmptyInput(string input) {
    var tokens = GetTokens(input);
    Assert.That(tokens, Is.Empty);
  }

  [TestCase("12")]
  [TestCase("0")]
  [TestCase("5768485")]
  public void Lexer_ShouldHandleIntegers(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(type: TokenType.IntegerLiteral, value: input);
  }

  [TestCase("12.5")]
  [TestCase("0.001")]
  [TestCase("5.")]
  public void Lexer_ShouldHandleFloats(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.FloatLiteral, input);
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
    token.AssertTypeAndValue(TokenType.Keyword, input);
  }

  [TestCase("and")]
  [TestCase("or")]
  [TestCase("not")]
  [TestCase("mod")]
  public void Lexer_ShouldHandleWordOperators(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.Operator, input);
  }

  [TestCase("integer")]
  [TestCase("float")]
  [TestCase("string")]
  [TestCase("char")]
  [TestCase("boolean")]
  public void Lexer_ShouldHandleTypeLiterals(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.TypeLiteral, input);
  }

  [TestCase("true")]
  [TestCase("false")]
  public void Lexer_ShouldHandleBooleanLiterals(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.BooleanLiteral, input);
  }

  [TestCase("\"testing string\"")]
  public void Lexer_ShouldHandleStringLiterals(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.StringLiteral, input);
  }

  [TestCase("'a'")]
  [TestCase("'b'")]
  [TestCase("'j'")]
  public void Lexer_ShouldHandleCharLiterals(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.CharLiteral, input);
  }

  [TestCase(";")]
  [TestCase(":")]
  [TestCase(".")]
  [TestCase(",")]
  [TestCase("{")]
  [TestCase("}")]
  [TestCase("(")]
  [TestCase(")")]
  [TestCase("[")]
  [TestCase("]")]
  public void Lexer_ShouldHandlePunctuation(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.Punctuation, input);
  }

  [TestCase("+")]
  [TestCase("-")]
  [TestCase("*")]
  [TestCase("/")]
  [TestCase("<")]
  [TestCase(">")]
  public void Lexer_ShouldHandleCompoundAssigmentOperators(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.Operator, input);
    var assignmentToken = GetToken(input + "=");
    assignmentToken.AssertTypeAndValue(TokenType.Operator, input + "=");
  }

  [TestCase("!=")]
  [TestCase(":=")]
  [TestCase("=")]
  public void Lexer_ShouldHandleOperatorsWithEqualSign(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.Operator, input);
  }

  #endregion

  #region Edge Cases

  [TestCase("::=:")]
  public void Lexer_ShouldHandleEdgeCaseWithColon(string input) {
    var tokens = GetTokens(input);
    Assert.Multiple(() => {
      Assert.That(tokens, Has.Count.EqualTo(3));
      tokens[0].AssertTypeAndValue(TokenType.Punctuation, ":");
      tokens[1].AssertTypeAndValue(TokenType.Operator, ":=");
      tokens[2].AssertTypeAndValue(TokenType.Punctuation, ":");
    });
  }

  [TestCase("=+=*==/=<=>=<>!===")]
  public void Lexer_ShouldHandleEdgeCaseWithOperators(string input) {
    var tokens = GetTokens(input);
    tokens.AssertWithChunkTable(TokenType.Operator, [
      "=", "+=", "*=", "=", "/=", "<=", ">=", "<", ">", "!=", "=", "="
    ]);
  }

  #endregion

  #region Illegal Token Cases

  [TestCase("\"not closed string")]
  [TestCase("\"")]
  [TestCase("'a")]
  [TestCase("'")]
  [TestCase("!")]
  [TestCase("@#*&*&$(*&#$^*&^@#$&^")]
  public void Lexer_ShouldHangleIllegalTokens(string input) {
    var token = GetToken(input);
    token.AssertTypeAndValue(TokenType.Illegal, input);
  }

  #endregion

  #region Helper Methods

  private static List<Token> GetTokens(string input) {
    var reader = new StringReader(input);
    var lexer = new Lexer(reader);
    return lexer.ToList();
  }

  private static Token GetToken(string input) {
    var tokens = GetTokens(input);
    Assert.That(tokens, Has.Count.EqualTo(1));
    return tokens[0];
  }

  #endregion
}
