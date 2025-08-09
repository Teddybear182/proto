using System.Text.RegularExpressions;

namespace Proto.Compiler.Lexer;

public class Lexer(string sourceCode) {
  private int _lines = 1;
  private int _pos = 0;
  private readonly List<Token> _tokens = new List<Token>();

  private bool IsMatch(Match match, TokenType type) {
    if (match.Success) {
      _tokens.Add(new Token(type, match.Groups[1].Value, _lines, _pos));
      _pos += match.Length;
      return true;
    } else {
      return false;
    }
  }

  public List<Token> Tokenize() {
    String[] code = sourceCode.Trim().Split("\n");

    foreach (string line in code) {
      while (_pos < line.Length) {
        Match matchInteger = Regex.Match(line.Substring(_pos), TokenPatterns.Integer);
        if (IsMatch(matchInteger, TokenType.Integer)) {
          continue;
        }
        Match matchFloat = Regex.Match(line.Substring(_pos), TokenPatterns.Float);
        if (IsMatch(matchFloat, TokenType.Float)) {
          continue;
        }
        Match matchChar = Regex.Match(line.Substring(_pos), TokenPatterns.Char);
        if (IsMatch(matchChar, TokenType.Char)) {
          continue;
        }
        Match matchString = Regex.Match(line.Substring(_pos), TokenPatterns.String);
        if (IsMatch(matchString, TokenType.String)) {
          continue;
        }
        Match matchBoolean = Regex.Match(line.Substring(_pos), TokenPatterns.Boolean);
        if (IsMatch(matchBoolean, TokenType.Boolean)) {
          continue;
        }
        Match matchOperator = Regex.Match(line.Substring(_pos), TokenPatterns.Operator);
        if (IsMatch(matchOperator, TokenType.Operator)) {
          continue;
        }
        Match matchTypes = Regex.Match(line.Substring(_pos), TokenPatterns.Types);
        if (IsMatch(matchTypes, TokenType.Types)) {
          continue;
        }
        Match matchKeyword = Regex.Match(line.Substring(_pos), TokenPatterns.Keyword);
        if (IsMatch(matchKeyword, TokenType.Keyword)) {
          continue;
        }
        Match matchPunctuation = Regex.Match(line.Substring(_pos), TokenPatterns.Punctuation);
        if (IsMatch(matchPunctuation, TokenType.Punctuation)) {
          continue;
        }
        Match matchWhitespace = Regex.Match(line.Substring(_pos), TokenPatterns.Whitespace);
        if (matchWhitespace.Success) {
          _pos += matchWhitespace.Length;
        }
      }

      _lines++;
      _pos = 0;
    }
    _tokens.Add(new Token(TokenType.EOF, String.Empty, _lines, _pos));
    return _tokens;
  }

}
