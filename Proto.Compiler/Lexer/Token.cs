namespace Proto.Compiler.Lexer;

public readonly struct Token(TokenType type, string value, uint line, uint column) {
  public override string ToString() {
    return $"{type} \"{value}\" at line {line}, column {column}";
  }
}
