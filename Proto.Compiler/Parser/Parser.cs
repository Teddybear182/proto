namespace Proto.Compiler.Parser;

using Proto.Compiler.Lexer;
using AST.Statements;
using AST.Expressions;

public class Parser {
  private List<Token> _tokenStream;
  private int _pos = 0;

  #region Variables for binary operators
  private readonly Dictionary<string, OperatorType> _operators = new Dictionary<string, OperatorType> {
    { "+", OperatorType.Plus},
    { "-", OperatorType.Minus},
    { "*", OperatorType.Multiply},
    { "/", OperatorType.Divide},
    { "mod", OperatorType.Modulo},
  };

  private readonly string[] _additiveOperators = ["+", "-"];
  private readonly string[] _multiplicativeOperators = ["*", "/", "mod"];
  #endregion

  public Parser(List<Token> tokenStream) {
    this._tokenStream = tokenStream;
  }

  #region Helper methods
  private Token Check() {
    return this._tokenStream[this._pos];
  }

  private Token Next() {
    return this._tokenStream[this._pos];
    this._pos++;
  }

  private TokenType CheckType() {
    return this._tokenStream[this._pos].Type;
  }

  private string CheckValue() {
    return this._tokenStream[this._pos].Value;
  }

  private void Expect(TokenType expected, string errorMessage) {
    if (this._tokenStream[this._pos].Type != expected) {
      // TODO: this should be changed in the future
      Console.WriteLine(errorMessage + ", " + this._tokenStream[this._pos].ToString());
      return;
    }

    this.Next();
  }

  private bool CheckIf(TokenType expected) {
    return this._tokenStream[this._pos].Type == expected;
  }
  #endregion

  public ProgramStatement Generate() {
    var program = new ProgramStatement([]);
    while (this.CheckType() != TokenType.Eof) {
      program.Body.Append(ParseStatement());
    }

    return program;
  }

  private Statement ParseStatement() {
    // TODO: change this method in the future
    return new BlockStatement([]);
  }

  private Expression ParseExpression() {
    return this.ParseAdditiveExpression();
  }

  // 2 + 4
  private Expression ParseAdditiveExpression() {
    var left = this.ParseMultiplicativeExpression();

    if (this._additiveOperators.Contains(this.CheckValue())) {
      var op = new OperatorLiteral(this._operators[this.Next().Value]);
      var right = this.ParseMultiplicativeExpression();
      left = new BinaryExpression(op, left, right);
    }

    return left;
  }

  private Expression ParseMultiplicativeExpression() {
    var left = this.ParsePrimaryExpression();

    if (this._multiplicativeOperators.Contains(this.CheckValue())) {
      var op = new OperatorLiteral(this._operators[this.Next().Value]);
      var right = this.ParsePrimaryExpression();
      left = new BinaryExpression(op, left, right);
    }

    return left;
  }

  private Expression ParsePrimaryExpression() {
    var token = this.Check();
    switch (token.Type) {
      case TokenType.Identifier:
        return new IdentifierExpression(token.Value);
      case TokenType.IntegerLiteral:
        return new IntegerLiteral(int.Parse(token.Value));
      case TokenType.FloatLiteral:
        return new FloatLiteral(float.Parse(token.Value));
      case TokenType.StringLiteral:
        return new StringLiteral(token.Value);
      default:
        return new IntegerLiteral(0);
    }
  }
}
