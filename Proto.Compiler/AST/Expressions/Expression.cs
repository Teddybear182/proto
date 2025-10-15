namespace Proto.Compiler.AST.Expressions;

public abstract record Expression(NodeType Type) : Node(Type);
