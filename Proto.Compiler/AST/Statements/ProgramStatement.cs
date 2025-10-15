namespace Proto.Compiler.AST.Statements;

public record ProgramStatement(Statement[] Body) : Node(NodeType.Program);
