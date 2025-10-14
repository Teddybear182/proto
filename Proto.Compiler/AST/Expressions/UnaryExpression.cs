namespace Proto.Compiler.AST.Expressions;

public record UnaryExpression(OperatorLiteral Operator, Expression Operand) : Expression(NodeType.UnaryExpression);
