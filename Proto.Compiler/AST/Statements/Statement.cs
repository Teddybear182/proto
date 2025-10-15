namespace Proto.Compiler.AST.Statements;

public abstract record Statement(NodeType Type) : Node(Type);
