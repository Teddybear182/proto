namespace Proto.Compiler.AST;

public record BooleanLiteral(bool Value) : Expression(NodeType.BooleanLiteralExpression);
