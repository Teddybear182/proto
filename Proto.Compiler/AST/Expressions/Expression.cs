namespace Proto.Compiler.AST;

public abstract record Expression(NodeType Type) : Node(Type);
