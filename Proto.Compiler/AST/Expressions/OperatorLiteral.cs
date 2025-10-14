namespace Proto.Compiler.AST.Expressions;

public record OperatorLiteral(OperatorType OperatorType) : Node(NodeType.OperatorLiteral);
