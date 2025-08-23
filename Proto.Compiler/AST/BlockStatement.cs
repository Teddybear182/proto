namespace Proto.Compiler.AST;

public record BlockStatement(Statement[] Body) : Statement(NodeType.BlockStatement);
