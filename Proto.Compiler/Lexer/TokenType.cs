namespace Proto.Compiler.Lexer;

public enum TokenType {
  // literals
  IntegerLiteral,
  FloatLiteral,
  StringLiteral,
  CharLiteral,
  BooleanLiteral,
  // main token types
  Identifier,
  Operator,
  Keyword,
  Punctuation,
  TypeLiteral,
  // special token types
  Eof,
  Illegal,
}

public static class TokenTypeExtensions {
  public static string ShortDescription(this TokenType tokenType) => tokenType switch {
    TokenType.IntegerLiteral => "integer literal",
    TokenType.FloatLiteral => "float literal",
    TokenType.StringLiteral => "string literal",
    TokenType.CharLiteral => "char literal",
    TokenType.BooleanLiteral => "boolean literal",
    TokenType.Identifier => "identifier",
    TokenType.Operator => "operator",
    TokenType.Keyword => "keyword",
    TokenType.Punctuation => "punctuation",
    TokenType.TypeLiteral => "type literal",
    TokenType.Eof => "end of file",
    TokenType.Illegal => "illegal token"
  };
}
