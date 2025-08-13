namespace Proto.Compiler.AST;

public enum NodeType {
  BinaryExpression,
  UnaryExpression,
  AssignmentExpression,
  CallExpression,
  IntegerLiteral,
  FloatLiteral,
  StringLiteral,
  CharLiteral,
  BooleanLiteral,
  Program,
  FuncDeclaration,
  VarDeclaration,
  IfStatement,
  LoopStatement,
  BlockStatement,
  BreakStatement,
  ContinueStatement,
  ReturnStatement,
}

public enum Type {
  Integer,
  Float,
  String,
  Char,
  Boolean,
}

public abstract record Node(NodeType Type); // main node, placeholder for every node

// statement and expression nodes are just placeholders for other nodes
public abstract record Statement(NodeType Type) : Node(Type);
public abstract record Expression(NodeType Type) : Node(Type);


//Statements
public abstract record Program(Statement[] Body) : Node(NodeType.Program);

public record FuncDeclaration(
  string Name,
  Argument[]? Arguments,
  NodeType? ReturnType,
  BlockStatement Body
) : Statement(NodeType.FuncDeclaration);

public record Argument( // argument node that contains name and type
  string Name,
  NodeType ArgType
);

public record VarDeclaration(
  string Name,
  bool IsConstant,
  Type VarType,
  Expression? InitValue
) : Statement(NodeType.VarDeclaration);

public record BlockStatement(Statement[] Body) : Statement(NodeType.BlockStatement);

public record IfStatement(
  Expression Condition,
  Statement Body
) : Statement(NodeType.IfStatement);

public record LoopStatement(
  Expression Condition,
  Statement Body
) : Statement(NodeType.LoopStatement);

public record BreakStatement() : Statement(NodeType.BreakStatement);

public record ContinueStatement() : Statement(NodeType.ContinueStatement);

public record ReturnStatement(Expression Value) : Statement(NodeType.ReturnStatement);


//Expressions
public record BinaryExpression(
  string Operator,
  Expression Left,
  Expression Right
) : Expression(NodeType.BinaryExpression);

public record UnaryExpression(string Operator, Expression Operand) : Expression(NodeType.UnaryExpression);

public record AssignmentExpression(Expression Left, Expression Right) : Expression(NodeType.AssignmentExpression);

public record CallExpression(Expression Caller, Expression Arguments) : Expression(NodeType.CallExpression);

public record IntegerLiteral(int Value) : Expression(NodeType.IntegerLiteral);

public record FloatLiteral(float Value) : Expression(NodeType.FloatLiteral);

public record StringLiteral(string Value) : Expression(NodeType.StringLiteral);

public record CharLiteral(char Value) : Expression(NodeType.CharLiteral);

public record BooleanLiteral(bool Value) : Expression(NodeType.BooleanLiteral);
