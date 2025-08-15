using System.Text;

namespace Proto.Compiler.Lexer;

public sealed class Lexer(TextReader reader) {
  private readonly ProgramSource _source = new ProgramSource(reader);
  private readonly LexerConstants _constants = new LexerConstants();

  public Token ReadToken() {
    _source.SkipWhitespace();
    _source.SkipNewLine();

    char currentChar = _source.ReadChar(); // "eats" the next char

    if (currentChar != '\0') {

      if (char.IsDigit(currentChar)) {
        return ReadNumberToken(currentChar);
      }

      if (char.IsLetter(currentChar)) {
        return ReadIdentifierToken(currentChar);
      }

      if (currentChar == '"') {
        return ReadStringToken();
      }

      if (currentChar == '\'') {
        return ReadCharToken();
      }

      if (_constants.Punctuation.Contains(currentChar)) {
        return new Token(TokenType.Punctuation, currentChar.ToString(), _source.GetLine(), _source.GetColumn());
      }

      switch (currentChar) {
        case '+':
        case '-':
        case '*':
        case '/':
        case '<':
        case '>':
          StringBuilder value = new StringBuilder();
          value.Append(currentChar);
          if (_source.PeekChar() == '=') {
            value.Append(_source.ReadChar());
          }
          return new Token(TokenType.Operator, value.ToString(), _source.GetLine(), _source.GetColumn());
        case '!':
        case ':':
          StringBuilder value2 = new StringBuilder();
          value2.Append(currentChar);
          if (_source.PeekChar() != '=') {
            return new Token(TokenType.Illegal, String.Empty, _source.GetLine(), _source.GetColumn()); // its must be either != or :=
          }
          value2.Append(_source.ReadChar());
          return new Token(TokenType.Operator, value2.ToString(), _source.GetLine(), _source.GetColumn());
        case '=':
        case '%':
          return new Token(TokenType.Operator, currentChar.ToString(), _source.GetLine(), _source.GetColumn());
      }

      return new Token(TokenType.Undefined, currentChar.ToString(), _source.GetLine(), _source.GetColumn());
    }

    return new Token(TokenType.EOF, String.Empty, _source.GetLine(), _source.GetColumn());
  }

  private Token ReadNumberToken(char currentChar) {
    StringBuilder value = new StringBuilder();
    value.Append(ReadInteger(currentChar));

    if (_source.PeekChar() != '.') {
      return new Token(TokenType.IntegerLiteral, value.ToString(), _source.GetLine(), _source.GetColumn());
    }

    value.Append(_source.ReadChar()); // appends dot to the value

    char afterDot = _source.PeekChar();
    if (char.IsDigit(afterDot)) {
      value.Append(ReadInteger(_source.ReadChar()));
      return new Token(TokenType.FloatLiteral, value.ToString(), _source.GetLine(), _source.GetColumn());
    }

    return new Token(TokenType.Illegal, String.Empty, _source.GetLine(), _source.GetColumn());
  }

  private string ReadInteger(char currentChar) {
    StringBuilder value = new StringBuilder();
    value.Append(currentChar);

    char nextChar = _source.PeekChar();
    while (nextChar != '\0' && char.IsDigit(nextChar)) {
      _source.Eat();
      value.Append(nextChar);
      nextChar = _source.PeekChar();
    }

    return value.ToString();
  }

  private Token ReadIdentifierToken(char currentChar) {
    StringBuilder value = new StringBuilder();
    value.Append(currentChar);

    char nextChar = _source.PeekChar();
    while (nextChar != '\0' && char.IsLetterOrDigit(nextChar)) {
      _source.Eat();
      value.Append(nextChar);
      nextChar = _source.PeekChar();
    }

    if (_constants.Keywords.Contains(value.ToString())) {
      return new Token(TokenType.Keyword, value.ToString(), _source.GetLine(), _source.GetColumn());
    }

    if (_constants.Bool.Contains(value.ToString())) {
      return new Token(TokenType.BooleanLiteral, value.ToString(), _source.GetLine(), _source.GetColumn());
    }

    if (_constants.Types.Contains(value.ToString())) {
      return new Token(TokenType.Type, value.ToString(), _source.GetLine(), _source.GetColumn());
    }

    if (_constants.WordOperators.Contains(value.ToString())) {
      return new Token(TokenType.Operator, value.ToString(), _source.GetLine(), _source.GetColumn());
    }

    return new Token(TokenType.Identifier, value.ToString(), _source.GetLine(), _source.GetColumn());
  }

  private Token ReadStringToken() {
    StringBuilder value = new StringBuilder();
    char currentChar = _source.PeekChar();
    while (currentChar != '"') {
      if (char.IsLetterOrDigit(currentChar)) {
        _source.Eat();
        value.Append(currentChar);
      }

      if (currentChar == '\\') {
        _source.Eat();
        char nextChar =  _source.PeekChar();

        switch (nextChar) {
          case 'n':
            _source.Eat();
            value.Append('\n');
            break;
          case 't':
            _source.Eat();
            value.Append('\t');
            break;
          case '"':
            _source.Eat();
            value.Append('"');
            break;
          default:
            return new Token(TokenType.Illegal, String.Empty, _source.GetLine(), _source.GetColumn());
        }
      }
      currentChar = _source.PeekChar();
    }
    return new Token(TokenType.StringLiteral, value.ToString(), _source.GetLine(), _source.GetColumn());
  }

  private Token ReadCharToken() {
    StringBuilder value = new StringBuilder("");
    char currentChar = _source.PeekChar();
    if (char.IsLetterOrDigit(currentChar)) { // checks if char is valid
      _source.Eat();
      value.Append(currentChar);
    }

    if (char.IsLetterOrDigit(_source.PeekChar())) {
      return new Token(TokenType.Illegal, String.Empty, _source.GetLine(), _source.GetColumn());
    }

    return new Token(TokenType.CharLiteral, value.ToString(), _source.GetLine(), _source.GetColumn());
  }

}
