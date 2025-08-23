namespace Proto.Compiler.AST;

public record VarDeclarationStatement(
  string Name,
  bool IsConstant,
  TypeLiteral VarType,
  Expression? InitValue
) : Statement(NodeType.VarDeclarationStatement);
