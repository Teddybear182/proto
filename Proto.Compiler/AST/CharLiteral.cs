namespace Proto.Compiler.AST;

public record CharLiteral(char Value) : Expression(NodeType.CharLiteralExpression);
