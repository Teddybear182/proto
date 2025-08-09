namespace Proto.Compiler.Lexer;

public struct Token(TokenType type, string value, int line, int column) {
  public override string ToString() {
    return $"Token => type: {type}, value: {value}, line: {line}, column: {column}";
  }
}
