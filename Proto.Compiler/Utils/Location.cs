namespace Proto.Compiler.Utils;

public readonly record struct Location(uint Line, uint Column, uint Offset) {
  public override string ToString() {
    return $"at line {this.Line}, at column {this.Column}";
  }
};
