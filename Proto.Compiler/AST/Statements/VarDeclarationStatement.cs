using Proto.Compiler.AST.Expressions;

namespace Proto.Compiler.AST.Statements;

public record VarDeclarationStatement(
  string Name,
  bool IsConstant,
  TypeLiteral VarType,
  Expression? InitValue
) : Statement(NodeType.VarDeclarationStatement);
