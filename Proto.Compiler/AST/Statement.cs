namespace Proto.Compiler.AST;

public abstract record Statement(NodeType Type) : Node(Type);
