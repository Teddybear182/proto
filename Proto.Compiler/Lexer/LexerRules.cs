namespace Proto.Compiler.Lexer;

using System.Collections.Frozen;

file static class WordTokens {
  public static readonly string[] Keywords =
    ["break", "continue", "return", "var", "const", "begin", "end", "if", "then", "else", "task", "while"];
  public static readonly string[] Operators =
    ["and", "or", "not", "mod"];
  public static readonly string[] TypeLiterals =
    ["integer", "float", "string", "char", "boolean"];
  public static readonly string[] BooleanLiterals =
    ["true", "false"];
}

internal static class LexerRules {
  private const char StringLiteralFirstSymbol = '"';
  private const char CharLiteralFirstSymbol = '\'';
  private const char EqualSign = '=';
  private const char DotSign = '.';
  private const char UnderscoreSymbol = '_';

  private static readonly FrozenDictionary<string, TokenType> KeywordToTokenTypeLookup;
  private static readonly char[] PunctuationSymbols = [';', ':', '.', ',', '{', '}', '(', ')', '[', ']'];
  private static readonly char[] CompoundAssignmentOperatorInitialSymbols = ['+', '-', '*', '/', '<', '>'];
  private static readonly char[] OperatorsWithEqualSignInitialSymbols = ['!', ':'];
  private static readonly char[] SingleCharOperators = ['='];

#pragma warning disable
  static LexerRules() {
    var keywordLookup = new Dictionary<string, TokenType>();
    RegisterWordType(TokenType.Keyword, WordTokens.Keywords);
    RegisterWordType(TokenType.Operator, WordTokens.Operators);
    RegisterWordType(TokenType.TypeLiteral, WordTokens.TypeLiterals);
    RegisterWordType(TokenType.BooleanLiteral, WordTokens.BooleanLiterals);
    KeywordToTokenTypeLookup = keywordLookup.ToFrozenDictionary();
    return;

    void RegisterWordType(TokenType type, IEnumerable<string> words) {
      foreach (var word in words) {
        keywordLookup.Add(word, type);
      }
    }
  }
#pragma warning restore

  public static TokenType GetWordTokenType(string word) {
    return KeywordToTokenTypeLookup.GetValueOrDefault(word, TokenType.Identifier);
  }

  #region Constant Rules

  public static bool IsStringLiteralFirstSymbol(char? symbol) {
    return symbol == StringLiteralFirstSymbol;
  }

  public static bool IsCharLiteralFirstSymbol(char? symbol) {
    return symbol == CharLiteralFirstSymbol;
  }

  public static bool IsEqualSign(char? symbol) {
    return symbol == EqualSign;
  }

  public static bool IsDotSign(char? symbol) {
    return symbol == DotSign;
  }

  #endregion

  #region Lookup Rules

  public static bool IsPunctuation(char symbol) {
    return PunctuationSymbols.Contains(symbol);
  }

  public static bool IsCompoundOperatorSymbol(char symbol) {
    return CompoundAssignmentOperatorInitialSymbols.Contains(symbol);
  }

  public static bool IsInitialSymbolOfEqualSignOperator(char symbol) {
    return OperatorsWithEqualSignInitialSymbols.Contains(symbol);
  }

  public static bool IsSingleCharOperator(char symbol) {
    return SingleCharOperators.Contains(symbol);
  }

  #endregion

  #region Char Rules

  public static bool IsBeginningOfNumber(char symbol) {
    return char.IsAsciiDigit(symbol);
  }

  public static bool IsBeginningOfWord(char symbol) {
    return char.IsAsciiLetter(symbol);
  }

  public static bool IsWordSymbol(char symbol) {
    return char.IsAsciiLetterOrDigit(symbol) || symbol == UnderscoreSymbol;
  }

  public static bool IsWhitespace(char symbol) {
    return char.IsWhiteSpace(symbol);
  }

  public static bool IsDigit(char symbol) {
    return char.IsAsciiDigit(symbol);
  }

  #endregion
}
