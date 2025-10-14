namespace Proto.Compiler.AST.Statements;

public record BlockStatement(Statement[] Body) : Statement(NodeType.BlockStatement);
