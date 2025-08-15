namespace Proto.Compiler.Lexer;

using System.Collections.Frozen;

internal static class LexerConstants {
  public static readonly string[] TrueKeywords = ["break", "continue", "return", "var", "const", "begin", "end", "if", "then", "else", "task", "while"];
  public static readonly char[] PunctuationSymbols = [';', ':', '.', ',', '{', '}', '(', ')', '[', ']'];
  public static readonly string[] TrueOperators = ["+", "-", "*", "/", "%", ":=", "!=", "=", "<=", ">=", "<", ">"];
  public static readonly string[] KeywordOperators = ["and", "or", "not"];
  public static readonly string[] TypeLiterals = ["integer", "float", "string", "char", "boolean"];
  public static readonly string[] BoolLiterals = ["true", "false"];

  public static readonly FrozenDictionary<string, TokenType> KeywordToTokenTypeLookup;

#pragma warning disable
  static LexerConstants() {
    var keywordLookup = new Dictionary<string, TokenType>();
    foreach (var trueKeyword in TrueKeywords) {
      keywordLookup.Add(trueKeyword, TokenType.Keyword);
    }
    foreach (var keywordOperator in KeywordOperators) {
      keywordLookup.Add(keywordOperator, TokenType.Operator);
    }
    foreach (var typeLiteral in TypeLiterals) {
      keywordLookup.Add(typeLiteral, TokenType.TypeLiteral);
    }
    foreach (var boolLiteral in BoolLiterals) {
      keywordLookup.Add(boolLiteral, TokenType.BooleanLiteral);
    }
    KeywordToTokenTypeLookup = keywordLookup.ToFrozenDictionary();
  }
#pragma warning restore
}
