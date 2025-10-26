namespace Proto.Compiler.Parser;

using Lexer;
using AST.Statements;
using AST.Expressions;

public class Parser {

  private readonly ParserTools _parserTools;

  public Parser(List<Token> tokenStream) {
    this._parserTools = new ParserTools(tokenStream);
  }

  public ProgramStatement Generate() {
    var program = new ProgramStatement([]);
    while (this._parserTools.CheckType() != TokenType.Eof) {
      program.Body.Append(ParseStatement());
    }

    return program;
  }

  #region Parsing statements
  private Statement ParseStatement() {
    // TODO: change this method in the future
    return this.ParseBlockStatement();
  }

  private BlockStatement ParseBlockStatement() {
    if (this._parserTools.CheckValue() == "{") {
      this._parserTools.Next();
      var block = new BlockStatement(this.ParseStatementList());
      this._parserTools.Next();
      return block;
    }

    return new BlockStatement([]);
  }

  private Statement[] ParseStatementList() {
    var statementList = new Statement[] {};
    while (this._parserTools.CheckType() != TokenType.Eof && this._parserTools.CheckValue() != "}") {
      statementList.Append(ParseStatement());
    }
    return statementList;
  }

  #endregion



  #region Parsing expressions
  private Expression ParseExpression() {
    return this.ParseBinaryExpression(this.ParseOrExpression, ParserUtils.Or);
  }

  private Expression ParseBinaryExpression(Func<Expression> next, string[] operators) {
    var left = next();

    while (this._parserTools.CheckType() == TokenType.Operator && operators.Contains(this._parserTools.CheckValue())) {
      var nextToken = this._parserTools.Next();
      var op = new OperatorLiteral(ParserUtils.Operators[nextToken.Value]);
      var right = next();
      left = new BinaryExpression(op, left, right);
    }

    return left;
  }

  private Expression ParseOrExpression() {
    return this.ParseBinaryExpression(this.ParseAndExpression, ParserUtils.And);
  }

  private Expression ParseAndExpression() {
    return this.ParseBinaryExpression(this.ParseComparisonExpression1, ParserUtils.ComparisonOperators1);
  }

  private Expression ParseComparisonExpression1() {
    return this.ParseBinaryExpression(this.ParseComparisonExpression2, ParserUtils.ComparisonOperators2);
  }

  private Expression ParseComparisonExpression2() {
    return this.ParseBinaryExpression(this.ParseAdditiveExpression, ParserUtils.AdditiveOperators);
  }

  private Expression ParseAdditiveExpression() {
    return this.ParseBinaryExpression(this.ParseMultiplicativeExpression, ParserUtils.MultiplicativeOperators);
  }

  private Expression ParseMultiplicativeExpression() {
    return this.ParseUnaryExpression();
  }

  private Expression ParseUnaryExpression() {
    if (ParserUtils.UnaryOperators.Contains(this._parserTools.CheckValue())) {
      var op = new OperatorLiteral(ParserUtils.Operators[this._parserTools.Next().Value]);
      var operand = this.ParsePrimaryExpression();
      return new UnaryExpression(op, operand);
    }

    return this.ParsePrimaryExpression();
  }

  private Expression ParsePrimaryExpression() {
    var token = this._parserTools.Check();
    switch (token.Type) {

      case TokenType.Identifier:
        this._parserTools.Next();
        return new IdentifierExpression(token.Value);

      case TokenType.IntegerLiteral:
        this._parserTools.Next();
        return new IntegerLiteral(int.Parse(token.Value));

      case TokenType.FloatLiteral:
        this._parserTools.Next();
        return new FloatLiteral(float.Parse(token.Value));

      case TokenType.StringLiteral:
        this._parserTools.Next();
        return new StringLiteral(token.Value);

      case TokenType.CharLiteral:
        this._parserTools.Next();
        return new CharLiteral(char.Parse(token.Value));

      case TokenType.BooleanLiteral:
        this._parserTools.Next();
        return new BooleanLiteral(bool.Parse(token.Value));

      case TokenType.Punctuation:
        this._parserTools.ExpectValue("(", token.Location, $"Expected open parenthesis instead of {token.ToString()}");
        var expression = this.ParseExpression();
        this._parserTools.ExpectValue(")", token.Location, $"Expected end of expression");
        return expression;

      default:
        throw new ParserException($"Unexpected token: {token.ToString()}", token.Location);
    }
  }
  #endregion
}
