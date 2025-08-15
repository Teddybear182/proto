namespace Proto.Compiler.Utils;

public readonly record struct Location(uint Line, uint Column) {
  public override string ToString() {
    return $"at line {this.Line}, at column {this.Column}";
  }
};
