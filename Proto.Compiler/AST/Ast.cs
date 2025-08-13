namespace Proto.Compiler.AST;

public enum NodeType { // enum for node types
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

public enum Type { // enum for types
  Integer,
  Float,
  String,
  Char,
  Boolean,
}

public abstract record Node() { // main node
  public readonly NodeType Type;
}

public abstract record Program : Node {
  public readonly NodeType Type = NodeType.Program;
  public readonly Statement[] Body = [];
}

public abstract record Statement : Node { // statement and expression nodes are just placeholders for other nodes
}

public abstract record Expression : Node {
}


//Statements
public record FuncDeclaration : Statement {
  public readonly NodeType Type = NodeType.FuncDeclaration;
  public readonly string Name;
  public readonly Argument[]? Arguments; // there may be no args
  public readonly NodeType? ReturnType; // if there is no return type, then its considered void
  public readonly BlockStatement Body;
}

public record Argument : Statement { // argument node that contains name and type
  public readonly string Name;
  public readonly NodeType Type;
}

public record VarDeclaration : Statement {
  public readonly NodeType Type = NodeType.VarDeclaration;
  public readonly string Name;
  public readonly bool IsConstant;
  public readonly Type VarType;
  public readonly Expression? InitValue; // there may be no initial value
}

public record BlockStatement : Statement {
  public readonly NodeType Type = NodeType.BlockStatement;
  public readonly Statement[] Body = [];
}

public record IfStatement : Statement {
  public readonly NodeType Type = NodeType.IfStatement;
  public readonly Expression Condition;
  public readonly Statement Body;
}

public record LoopStatement : Statement {
  public readonly NodeType Type = NodeType.LoopStatement;
  public readonly Expression Condition;
  public readonly Statement Body;
}

public record BreakStatement : Statement {
  public readonly NodeType Type = NodeType.BreakStatement;
}

public record ContinueStatement : Statement {
  public readonly NodeType Type = NodeType.ContinueStatement;
}

public record ReturnStatement : Statement {
  public readonly NodeType Type = NodeType.ReturnStatement;
  public readonly Expression Value;
}

//Expressions
public record BinaryExpression : Expression {
  public readonly NodeType Type = NodeType.BinaryExpression;
  public readonly string Operator;
  public readonly Expression Left;
  public readonly Expression Right;
}

public record UnaryExpression : Expression {
  public readonly NodeType Type = NodeType.UnaryExpression;
  public readonly string Operator;
  public readonly Expression Operand;
}

public record AssignmentExpression : Expression {
  public readonly NodeType Type = NodeType.AssignmentExpression;
  public readonly Expression Left;
  public readonly Expression Right;
}

public record CallExpression : Expression {
  public readonly NodeType Type = NodeType.CallExpression;
  public readonly Expression Caller;
  public readonly Expression Arguments;
}

public record IntegerLiteral : Expression {
  public readonly NodeType Type = NodeType.IntegerLiteral;
  public readonly int Value;
}

public record FloatLiteral : Expression {
  public readonly NodeType Type = NodeType.FloatLiteral;
  public readonly float Value;
}

public record StringLiteral : Expression {
  public readonly NodeType Type = NodeType.StringLiteral;
  public readonly string Value;
}

public record CharLiteral : Expression {
  public readonly NodeType Type = NodeType.CharLiteral;
  public readonly char Value;
}

public record BooleanLiteral : Expression {
  public readonly NodeType Type = NodeType.BooleanLiteral;
  public readonly bool Value;
}
