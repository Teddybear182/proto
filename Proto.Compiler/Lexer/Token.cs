namespace Proto.Compiler.Lexer;

using Proto.Compiler.Utils;

public readonly record struct Token(TokenType Type, string Value, Location Location) {
  public override string ToString() {
    return $"{this.Type.ShortDescription()} \"{this.Value}\" {this.Location}";
  }
}
