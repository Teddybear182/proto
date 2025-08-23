namespace Proto.Compiler.AST;

public record BinaryExpression(
  string Operator,
  Expression Left,
  Expression Right
) : Expression(NodeType.BinaryExpression);
