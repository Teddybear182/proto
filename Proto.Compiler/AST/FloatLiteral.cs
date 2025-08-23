namespace Proto.Compiler.AST;

public record FloatLiteral(float Value) : Expression(NodeType.FloatLiteralExpression);
