namespace Proto.Compiler.AST.Expressions;

public record BinaryExpression(
  OperatorLiteral Operator,
  Expression Left,
  Expression Right
) : Expression(NodeType.BinaryExpression);
