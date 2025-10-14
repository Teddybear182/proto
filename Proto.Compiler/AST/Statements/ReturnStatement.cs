using Proto.Compiler.AST.Expressions;

namespace Proto.Compiler.AST.Statements;

public record ReturnStatement(Expression? Value) : Statement(NodeType.ReturnStatement);
