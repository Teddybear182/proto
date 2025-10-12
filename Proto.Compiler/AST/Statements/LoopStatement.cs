namespace Proto.Compiler.AST;

public record LoopStatement(
  Expression Condition,
  Statement Body
) : Statement(NodeType.LoopStatement);
