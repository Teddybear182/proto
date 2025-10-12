namespace Proto.Compiler.AST;

public record OperatorLiteral(OperatorType OperatorType) : Node(NodeType.OperatorLiteral);
