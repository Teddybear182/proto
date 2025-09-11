namespace Proto.Compiler.AST;

public record StringLiteral(string Value) : Expression(NodeType.StringLiteralExpression);
