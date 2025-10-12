namespace Proto.Compiler.AST;

public record FuncDeclarationStatement(
  string Name,
  Parameter[]? Parameters,
  TypeLiteral? ReturnType,
  BlockStatement Body
) : Statement(NodeType.FuncDeclarationStatement);
