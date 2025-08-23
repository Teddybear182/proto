namespace Proto.Compiler.AST;

public record UnaryExpression(string Operator, Expression Operand) : Expression(NodeType.UnaryExpression);
