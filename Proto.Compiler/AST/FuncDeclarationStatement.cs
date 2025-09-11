namespace Proto.Compiler.AST;

public record FuncDeclarationStatement(
  string Name,
  Argument[]? Arguments,
  TypeLiteral? ReturnType,
  BlockStatement Body
) : Statement(NodeType.FuncDeclarationStatement);
