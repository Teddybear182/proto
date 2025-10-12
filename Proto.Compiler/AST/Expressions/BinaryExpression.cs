namespace Proto.Compiler.AST;

public record BinaryExpression(
  OperatorLiteral Operator,
  Expression Left,
  Expression Right
) : Expression(NodeType.BinaryExpression);
