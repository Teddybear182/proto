namespace Proto.Compiler.AST.Expressions;

public record CallExpression(Expression Caller, Expression[]? Arguments) : Expression(NodeType.CallExpression);
