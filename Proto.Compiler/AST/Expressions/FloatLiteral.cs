namespace Proto.Compiler.AST.Expressions;

public record FloatLiteral(float Value) : Expression(NodeType.FloatLiteralExpression);
