namespace Proto.Compiler.AST;

public abstract record Node(NodeType Type) {
  public override string ToString() {
    return $"{Type}";
  }
}
