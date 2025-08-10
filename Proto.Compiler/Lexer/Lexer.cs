using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.RegularExpressions;

namespace Proto.Compiler.Lexer;

public class Lexer() {
  private int _lines = 1;
  private int _pos = 0;
  private readonly TextReader _reader = TextReader.Null;

  private readonly List<string> _keywords = ["break","continue","return","var","const","begin","end","if","then","else","task","while" ];
  private readonly List<char> _punctuation = [';', ':', '.', ',', '{', '}', '(', ')', '[', ']'];
  private readonly List<string> _operators = ["+", "-", "*", "/", "%", ":=", "!=", "=", "<=", ">=", "<", ">", "and", "or", "not"];
  private readonly List<string> _types =  ["integer", "float", "string", "char", "boolean"];
  private readonly List<string> _bool =  ["true", "false"];

  public Lexer(TextReader reader) : this() {
    _reader = reader;
  }

  private char Peek() { // reads without advancing to the next char
    int peek = _reader.Peek();
    if (peek != -1) {
      return (char)_reader.Peek();
    } else {
      return '\0';
    }
  }

  private char Read() { // reads the char and advances to the next char
    int read = _reader.Read();
    if (read != -1) {
      return (char)read;
    } else {
      return '\0';
    }
  }

  private Token ReadToken() {
    char currentChar = Read(); // "eats" the next char


    if (currentChar != '\0') {

      if (currentChar == '\n') {
        _lines++;
        return ReadToken();
      }

      if (currentChar == '\r') {
        _lines++;
        return ReadToken();
      }

      if (char.IsWhiteSpace(currentChar)) { // just adding 1 to the pos cause Read() function already advanced to the next token so whitespace gonna be ignored
        _pos++;
        return ReadToken();
      }

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

      if (_punctuation.Contains(currentChar)) {
        return new Token(TokenType.Punctuation, currentChar.ToString(), _lines, _pos);
      }

      return new Token(TokenType.Undefined, currentChar.ToString(), _lines, _pos);
    } else {
      return new Token(TokenType.EOF, String.Empty, _lines, _pos);
    }
  }

  private Token ReadNumberToken(char currentChar) {
    StringBuilder value = new StringBuilder();
    value.Append(currentChar);
    _pos++;

    char nextChar = Read();
    while (nextChar != '\0' && char.IsDigit(nextChar)) { // \0 represents null and is not digit
      value.Append(nextChar);
      _pos++;
      nextChar = Read();
    }

    bool isFloat = false;

    if (nextChar == '.') { //checks for float
      char afterDot = Peek();
      if (char.IsDigit(afterDot)) { // checks if there is a digit after dot
        isFloat = true;
        value.Append(nextChar); // appends '.' to the value
        _pos++;
        afterDot = Read();
        while (afterDot != '\0' && char.IsDigit(afterDot)) {
          value.Append(afterDot);
          _pos++;
          afterDot = Read();
        }
      }

    }

    if (isFloat) {
      return new Token(TokenType.FloatLiteral, value.ToString(), _lines, _pos);
    }
    return new Token(TokenType.IntegerLiteral, value.ToString(), _lines, _pos);
  }

  private Token ReadIdentifierToken(char currentChar) {
    StringBuilder value = new StringBuilder();
    while (char.IsLetterOrDigit(currentChar)) { // adds next chars until whitespace or new line
      value.Append(currentChar);
      _pos++;
      currentChar = Read();
    }

    if (_operators.Contains(value.ToString())) {
      return new Token(TokenType.Operator, value.ToString(), _lines, _pos);
    }

    if (_keywords.Contains(value.ToString())) {
      return new Token(TokenType.Keyword, value.ToString(), _lines, _pos);
    }

    if (_bool.Contains(value.ToString())) {
      return new Token(TokenType.BooleanLiteral, value.ToString(), _lines, _pos);
    }

    if (_types.Contains(value.ToString())) {
      return new Token(TokenType.Type, value.ToString(), _lines, _pos);
    }

    return new Token(TokenType.Identifier, value.ToString(), _lines, _pos);
  }

  private Token ReadStringToken() {
    StringBuilder value = new StringBuilder();
    char currentChar = Read();
    while (char.IsLetterOrDigit(currentChar) && currentChar != '"') { // checks if content of the string is valid and if its not the end of the string
      value.Append(currentChar);
      _pos++;
      currentChar = Read();
    }
    return new Token(TokenType.StringLiteral, value.ToString(), _lines, _pos);
  }

  private Token ReadCharToken() {
    StringBuilder value = new StringBuilder("");
    char currentChar = Read();
    if (char.IsLetterOrDigit(currentChar)) { // checks if char is valid
      value.Append(currentChar);
      _pos++;
    }

    if (char.IsLetterOrDigit(Peek())) {
      Console.WriteLine("[Lexer error] Expected char literal, not string literal."); // idk if its needed
    }

    return new Token(TokenType.CharLiteral, value.ToString(), _lines, _pos);
  }

}
