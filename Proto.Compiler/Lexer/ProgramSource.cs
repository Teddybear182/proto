namespace Proto.Compiler.Lexer;

using Proto.Compiler.Utils;

internal sealed class ProgramSource(TextReader reader) : IPeekable<char?> {
  private const int StartingLine = 1;
  private const int StartingColumn = 0;

  public uint Line { get; private set; } = StartingLine;
  public uint Column { get; private set; } = StartingColumn;

  public char? Peek() {
    var peeked = reader.Peek();
    if (peeked == -1) {
      return null;
    }
    return (char) peeked;
  }

  public char? Next() {
    var read = reader.Read();
    if (read == -1) {
      return null;
    }
    var charRead = (char) read;
    if (charRead == '\n') {
      this.Line++;
      this.Column = StartingColumn;
    } else {
      this.Column++;
    }
    return charRead;
  }

  public bool IsEof => this.Peek() == null;

  public Location Location => new(
    Line: this.Line,
    Column: this.Column
  );
}
