namespace Proto.Compiler.AST.Statements;

public abstract record ProgramStatement(Statement[] Body) : Node(NodeType.Program);
