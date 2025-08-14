namespace Proto.Compiler.Lexer;

internal sealed class ProgramSource(TextReader reader) {
  private const int StartingLine = 1;
  private const int StartingColumn = 0;

  private uint _line = StartingLine;
  private uint _column = StartingColumn;

  internal char PeekChar() {
    int peek = reader.Peek();
    if (peek != -1) {
      return (char) peek;
    }
    return '\0';
  }

  internal char ReadChar() {
    int read = reader.Read();
    if (read != -1) {
      _column++;
      return (char) read;
    }
    return '\0';
  }

  internal void Eat() {
    reader.Read();
    _column++;
  }

  internal uint GetLine() {
    return _line;
  }

  internal uint GetColumn() {
    return _column;
  }

  internal void SkipWhitespace() {
    while (char.IsWhiteSpace(PeekChar())) {
      ReadChar();
    }
  }

  internal void SkipNewLine() {
    if (PeekChar() == '\n') {
      reader.Read();
      _column = StartingColumn;
      _line++;
    }
  }

  internal bool IsEof() {
    return reader.Peek() == -1;
  }
}
