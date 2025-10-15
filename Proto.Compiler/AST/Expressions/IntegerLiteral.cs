namespace Proto.Compiler.AST.Expressions;

public record IntegerLiteral(int Value) : Expression(NodeType.IntegerLiteralExpression);
