namespace Proto.Compiler.AST;

public record IfStatement(
  Expression Condition,
  Statement Body
) : Statement(NodeType.IfStatement);
