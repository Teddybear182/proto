using Proto.Compiler.AST.Expressions;

namespace Proto.Compiler.AST.Statements;

public record LoopStatement(
  Expression Condition,
  Statement Body
) : Statement(NodeType.LoopStatement);
