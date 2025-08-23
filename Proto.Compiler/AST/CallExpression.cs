namespace Proto.Compiler.AST;

public record CallExpression(Expression Caller, Expression Arguments) : Expression(NodeType.CallExpression);
