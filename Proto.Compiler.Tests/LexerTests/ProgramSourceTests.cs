namespace Proto.Compiler.Tests.LexerTests;

using Proto.Compiler.Lexer;

[TestFixture]
public class ProgramSourceTests {
  [TestCase("test string")]
  public void ProgramSource_ShouldTraverseCorrectly(string input) {
    var source = new ProgramSource(new StringReader(input));
    var output = "";
    while (!source.IsEof) {
      output += source.NextAsserted();
    }
    Assert.That(output, Is.EqualTo(input));
  }

  [TestCase("abcd")]
  [TestCase("12345")]
  public void ProgramSource_ShouldHandlePeek(string input) {
    var source = new ProgramSource(new StringReader(input));
    Assert.Multiple(() => {
      Assert.That(source.Peek(), Is.EqualTo(input[0]));
      Assert.That(source.PeekAsserted(), Is.EqualTo(input[0]));
    });
  }

  [TestCase("break")]
  [TestCase("{}[]()")]
  public void ProgramSource_ShouldHandleNext(string input) {
    var source = new ProgramSource(new StringReader(input));
    Assert.Multiple(() => {
      Assert.That(source.Next(), Is.EqualTo(input[0]));
      Assert.That(source.Next(), Is.EqualTo(input[1]));
      Assert.That(source.Peek(), Is.EqualTo(input[2]));
    });
  }

  [TestCase("")]
  public void ProgramSource_ShouldHandleNullPeekAndNext(string input) {
    var source = new ProgramSource(new StringReader(input));
    Assert.Multiple(() => {
      Assert.That(source.Peek(), Is.Null);
      Assert.That(source.Next(), Is.Null);
      Assert.That(source.Peek(), Is.Null);
    });
  }

  [TestCase("")]
  public void ProgramSource_ShouldThrowOnFailedAssert(string input) {
    var source = new ProgramSource(new StringReader(input));
    Assert.Multiple(() => {
      Assert.Throws<InvalidOperationException>(() => {
        source.NextAsserted();
      });
      Assert.Throws<InvalidOperationException>(() => {
        source.PeekAsserted();
      });
    });
  }
}
