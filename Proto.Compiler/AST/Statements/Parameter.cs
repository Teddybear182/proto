using Proto.Compiler.AST.Expressions;

namespace Proto.Compiler.AST.Statements;

public record Parameter(
  string Name,
  TypeLiteral ArgType
);
