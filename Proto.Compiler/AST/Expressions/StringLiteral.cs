namespace Proto.Compiler.AST.Expressions;

public record StringLiteral(string Value) : Expression(NodeType.StringLiteralExpression);
