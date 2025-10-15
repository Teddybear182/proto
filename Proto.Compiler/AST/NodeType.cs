namespace Proto.Compiler.AST;

public enum NodeType {
  BinaryExpression,
  UnaryExpression,
  OperatorLiteral,
  AssignmentExpression,
  CallExpression,
  IntegerLiteralExpression,
  FloatLiteralExpression,
  StringLiteralExpression,
  CharLiteralExpression,
  BooleanLiteralExpression,
  IdentifierExpression,
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
