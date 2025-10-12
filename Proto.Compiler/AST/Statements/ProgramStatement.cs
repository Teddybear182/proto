namespace Proto.Compiler.AST;

public abstract record ProgramStatement(Statement[] Body) : Node(NodeType.Program);
