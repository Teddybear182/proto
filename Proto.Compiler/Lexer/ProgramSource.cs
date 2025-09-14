namespace Proto.Compiler.Lexer;

using Proto.Compiler.Utils;

public sealed class ProgramSource(TextReader reader) : IPeekable<char?> {
  private const int StartingLine = 1;
  private const int StartingColumn = 1;
  private const int StartingOffset = 0;

  public uint Line { get; private set; } = StartingLine;
  public uint Column { get; private set; } = StartingColumn;
  public uint Offset { get; private set; } = StartingOffset;

  public char? Peek() {
    var peeked = reader.Peek();
    if (peeked == -1) {
      return null;
    }
    return (char) peeked;
  }

  public char PeekAsserted() {
    var peekSymbol = this.Peek();
    if (peekSymbol is not { } nonNullPeekSymbol) {
      throw new InvalidOperationException("PeekAsserted() returned null character");
    }
    return nonNullPeekSymbol;
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
    this.Offset++;
    return charRead;
  }

  public char NextAsserted() {
    var nextSymbol = this.Next();
    if (nextSymbol is not { } nonNullNextSymbol) {
      throw new InvalidOperationException("NextAsserted() returned null character");
    }
    return nonNullNextSymbol;
  }

  public bool IsEof => this.Peek() == null;

  public Location Location => new(
    Line: this.Line,
    Column: this.Column,
    Offset: this.Offset
  );
}
