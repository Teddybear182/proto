namespace Proto.Compiler.Lexer;

internal sealed class LexerConstants {
  public readonly List<string> Keywords = ["break", "continue", "return", "var", "const", "begin", "end", "if", "then", "else", "task", "while"];
  public readonly List<char> Punctuation = [';', ':', '.', ',', '{', '}', '(', ')', '[', ']'];
  public readonly List<string> Operators = ["+", "-", "*", "/", "%", ":=", "!=", "=", "<=", ">=", "<", ">"];
  public readonly List<string> WordOperators = ["and", "or", "not"];
  public readonly List<string> Types = ["integer", "float", "string", "char", "boolean"];
  public readonly List<string> Bool = ["true", "false"];
}
