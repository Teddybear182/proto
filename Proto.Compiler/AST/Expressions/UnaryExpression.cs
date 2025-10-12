namespace Proto.Compiler.AST;

public record UnaryExpression(OperatorLiteral Operator, Expression Operand) : Expression(NodeType.UnaryExpression);
