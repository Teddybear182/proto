namespace Proto.Compiler.Parser;

using Proto.Compiler.Lexer;
using Proto.Compiler.Utils;

public class ParserTools(List<Token> tokerStream) {

  private readonly List<Token> _tokenStream = tokerStream;
  public int Pos = 0;

  #region Helper methods
  public Token Check() {
    return this._tokenStream[this.Pos];
  }

  public Token Next() {
    if (this.CheckType() != TokenType.Eof) {
      this.Pos++;
      return this._tokenStream[this.Pos];
    }

    return this.Check();
  }

  public TokenType CheckType() {
    return this.Check().Type;
  }

  public string CheckValue() {
    return this.Check().Value;
  }

  public void ExpectType(TokenType expected, Location pos, string errorMessage) {
    if (this._tokenStream[this.Pos].Type != expected) {
      // TODO: this should be changed in the future
      Console.WriteLine(errorMessage + ", " + pos);
      return;
    }

    this.Next();
  }

  public void ExpectValue(string expected, Location pos, string errorMessage) {
    if (this._tokenStream[this.Pos].Value != expected) {
      // TODO: this should be changed in the future
      Console.WriteLine(errorMessage + ", " + pos);
      return;
    }

    this.Next();
  }

  public bool CheckIf(TokenType expected) {
    return this._tokenStream[this.Pos].Type == expected;
  }
  #endregion

}
