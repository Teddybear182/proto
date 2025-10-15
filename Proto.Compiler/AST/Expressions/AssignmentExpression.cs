namespace Proto.Compiler.AST.Expressions;

public record AssignmentExpression(Expression Left, Expression Right) : Expression(NodeType.AssignmentExpression);
