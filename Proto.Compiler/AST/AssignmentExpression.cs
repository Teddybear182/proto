namespace Proto.Compiler.AST;

public record AssignmentExpression(Expression Left, Expression Right) : Expression(NodeType.AssignmentExpression);
