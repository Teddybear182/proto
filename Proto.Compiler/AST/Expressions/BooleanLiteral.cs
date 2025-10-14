namespace Proto.Compiler.AST.Expressions;

public record BooleanLiteral(bool Value) : Expression(NodeType.BooleanLiteralExpression);
