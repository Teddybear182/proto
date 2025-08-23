namespace Proto.Compiler.AST;

public record ReturnStatement(Expression Value) : Statement(NodeType.ReturnStatement);
