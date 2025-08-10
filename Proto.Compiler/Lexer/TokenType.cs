namespace Proto.Compiler.Lexer;

public enum TokenType {
  IntegerLiteral,
  FloatLiteral,
  StringLiteral,
  CharLiteral,
  BooleanLiteral,
  Identifier,
  Operator,
  Keyword,
  Punctuation,
  Type,
  EOF,
  Undefined
}
