namespace Proto.Compiler.AST.Expressions;

public record CharLiteral(char Value) : Expression(NodeType.CharLiteralExpression);
