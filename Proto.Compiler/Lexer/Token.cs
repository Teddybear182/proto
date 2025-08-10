namespace Proto.Compiler.Lexer;

public readonly struct Token(TokenType type, string value, int line, int column) {
  public override string ToString() {
    return $"{type} \"{value}\" at line {line}, column {column}";
  }
}
