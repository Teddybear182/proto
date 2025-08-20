namespace Proto.Compiler.Lexer;

using System.Diagnostics.Contracts;
using System.Text;

using Proto.Compiler.Utils;

public sealed class Lexer : Peekable<Token> {
  private readonly ProgramSource _source;
  private Location _tokenLocation;

  public Lexer(TextReader reader) {
    this._source = new ProgramSource(reader);
    this._tokenLocation = this._source.Location;
  }

  protected override Token ReadElement() {
    this.SkipWhitespace();
    var maybeCurrentChar = this._source.Peek();
    this._tokenLocation = this._source.Location;

    if (this._source.IsEof || maybeCurrentChar is not { } currentChar) {
      return this.Token(TokenType.Eof, string.Empty);
    }

    if (LexerRules.IsBeginningOfNumber(currentChar)) {
      return this.ReadNumberToken();
    }

    if (LexerRules.IsBeginningOfWord(currentChar)) {
      return this.ReadWordToken();
    }

    if (LexerRules.IsStringLiteralFirstSymbol(currentChar)) {
      return this.ReadStringToken();
    }

    if (LexerRules.IsCharLiteralFirstSymbol(currentChar)) {
      return this.ReadCharToken();
    }

    if (LexerRules.IsPunctuation(currentChar)) {
      return this.ReadSingleCharPunctuation();
    }

    if (LexerRules.IsCompoundOperatorSymbol(currentChar)) {
      return this.ReadCompoundAssignmentOperator();
    }

    if (LexerRules.IsInitialSymbolOfEqualSignOperator(currentChar)) {
      return this.ReadOperatorWithEqualSignEnding();
    }

    if (LexerRules.IsSingleCharOperator(currentChar)) {
      return this.ReadSingleCharOperator();
    }

    return this.ReadIllegalToken();
  }

  #region Lexer Helper Functions

  private string ReadWhile(Func<char, bool> predicate) {
    var output = new StringBuilder();
    while (!this._source.IsEof) {
      var peek = this._source.Peek();
      if (peek is { } charPeek) {
        if (!predicate(charPeek)) break;
        output.Append(charPeek);
      } else break;
    }
    return output.ToString();
  }

  private void SkipWhitespace() {
    this.ReadWhile(LexerRules.IsWhitespace);
  }

  private string ReadIntegerPart() {
    return this.ReadWhile(LexerRules.IsDigit);
  }

  [Pure]
  private Token Token(TokenType tokenType, string value) {
    return new Token(
      Type: tokenType,
      Value: value,
      Location: this._tokenLocation
    );
  }

  #endregion

  #region Token Functions

  private Token ReadCompoundAssignmentOperator() {
    var operatorLiteral = this._source.NextAsserted().ToString();
    if (LexerRules.IsEqualSign(this._source.Peek())) {
      operatorLiteral += this._source.NextAsserted();
    }
    return this.Token(TokenType.Operator, operatorLiteral);
  }

  private Token ReadOperatorWithEqualSignEnding() {
    var operatorLiteral = this._source.NextAsserted().ToString();
    if (!LexerRules.IsEqualSign(this._source.Peek())) {
      return this.Token(TokenType.Illegal, operatorLiteral);
    }
    operatorLiteral += this._source.NextAsserted();
    return this.Token(TokenType.Operator, operatorLiteral);
  }

  private Token ReadSingleCharOperator() {
    var operatorLiteral = this._source.NextAsserted().ToString();
    return this.Token(TokenType.Operator, operatorLiteral);
  }

  private Token ReadSingleCharPunctuation() {
    var punctuationLiteral = this._source.NextAsserted().ToString();
    return this.Token(TokenType.Punctuation, punctuationLiteral);
  }

  private Token ReadIllegalToken() {
    var illegalTokenLiteral = this.ReadWhile(symbol => !LexerRules.IsWhitespace(symbol));
    return this.Token(TokenType.Illegal, illegalTokenLiteral);
  }

  private Token ReadNumberToken() {
    var numberLiteral = this.ReadIntegerPart();
    if (!LexerRules.IsDotSign(this._source.Peek())) {
      return this.Token(TokenType.IntegerLiteral, numberLiteral);
    }
    numberLiteral += this._source.NextAsserted();
    numberLiteral += this.ReadIntegerPart();
    return this.Token(TokenType.FloatLiteral, numberLiteral);
  }

  private Token ReadWordToken() {
    var word = this.ReadWhile(LexerRules.IsWordSymbol);
    var type = LexerRules.GetWordTokenType(word);
    return this.Token(type, word);
  }

  // TODO: add special characters like \n and \t + quotes escaping
  private Token ReadStringToken() {
    var stringLiteral = this._source.NextAsserted().ToString();
    stringLiteral += this.ReadWhile(symbol => !LexerRules.IsStringLiteralFirstSymbol(symbol));
    if (!LexerRules.IsStringLiteralFirstSymbol(this._source.Peek())) {
      return this.Token(TokenType.Illegal, stringLiteral);
    }
    stringLiteral += this._source.NextAsserted();
    return this.Token(TokenType.StringLiteral, stringLiteral);
  }

  private Token ReadCharToken() {
    var charLiteral = this._source.NextAsserted().ToString();
    if (this._source.IsEof) {
      return this.Token(TokenType.Illegal, charLiteral);
    }
    charLiteral += this._source.NextAsserted();
    if (!LexerRules.IsCharLiteralFirstSymbol(this._source.Peek())) {
      return this.Token(TokenType.Illegal, charLiteral);
    }
    charLiteral += this._source.NextAsserted();
    return this.Token(TokenType.CharLiteral, charLiteral);
  }

  #endregion
}
