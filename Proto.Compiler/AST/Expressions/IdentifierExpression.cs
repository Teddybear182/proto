namespace Proto.Compiler.AST.Expressions;

public record IdentifierExpression(string Name) : Expression(NodeType.IdentifierExpression);
