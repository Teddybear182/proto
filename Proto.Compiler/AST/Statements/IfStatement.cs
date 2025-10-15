using Proto.Compiler.AST.Expressions;

namespace Proto.Compiler.AST.Statements;

public record IfStatement(
  Expression Condition,
  Statement Body
) : Statement(NodeType.IfStatement);
