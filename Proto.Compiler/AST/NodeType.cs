namespace Proto.Compiler.AST;

public enum NodeType {
  BinaryExpression,
  UnaryExpression,
  AssignmentExpression,
  CallExpression,
  IntegerLiteralExpression,
  FloatLiteralExpression,
  StringLiteralExpression,
  CharLiteralExpression,
  BooleanLiteralExpression,
  Program,
  FuncDeclarationStatement,
  VarDeclarationStatement,
  IfStatement,
  LoopStatement,
  BlockStatement,
  BreakStatement,
  ContinueStatement,
  ReturnStatement,
}
