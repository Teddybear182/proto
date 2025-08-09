namespace Proto.Compiler.Lexer;

public static class TokenPatterns {
  public const string Integer = @"^(\d+)";
  public const string Float = @"^(\d+\.\d+)";
  public const string Char = @"^'([A-Za-z])'";
  public const string String = @"^""([^""]*)""";
  public const string Boolean = @"^(true|false)";

  public const string Operator = @"^(\+|\-|\*|\/|%|:=|!=|=|<=|>=|<|>|and|or|not|)";
  public const string Punctuation = @"^([;:,.()\{\}\[\]])";
  public const string Keyword = @"^(break|continue|return|var|const|begin|end|if|then|else|task|while|)";
  public const string Types = @"^(integer|float|string|char|boolean|)";

  public const string Whitespace = @"^\s+";
}
