using Proto.Compiler.AST.Expressions;

namespace Proto.Compiler.AST.Statements;

public record FuncDeclarationStatement(
  string Name,
  Parameter[]? Parameters,
  TypeLiteral? ReturnType,
  BlockStatement Body
) : Statement(NodeType.FuncDeclarationStatement);
