namespace Proto.Compiler.AST;

public record IntegerLiteral(int Value) : Expression(NodeType.IntegerLiteralExpression);
