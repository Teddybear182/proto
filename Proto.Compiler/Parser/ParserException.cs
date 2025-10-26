namespace Proto.Compiler.Parser;
using Proto.Compiler.Utils;
public class ParserException : Exception {
  public ParserException(string message, Location pos) : base(FormatMessage(message, pos)) {
  }

  private static string FormatMessage(string message, Location pos) {
    return $"Parser exception: {message}, {pos.ToString()}";
  }
}
