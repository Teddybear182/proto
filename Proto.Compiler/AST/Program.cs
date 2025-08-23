namespace Proto.Compiler.AST;

public abstract record Program(Statement[] Body) : Node(NodeType.Program);
