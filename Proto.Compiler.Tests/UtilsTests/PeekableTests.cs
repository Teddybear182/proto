namespace Proto.Compiler.Tests.UtilsTests;

using Proto.Compiler.Utils;

file sealed class StringPeekable(string source) : Peekable<char?> {
  private int _pointer;

  protected override char? ReadElement() {
    if (this._pointer >= source.Length) {
      return null;
    }
    var element = source[this._pointer];
    this._pointer++;
    return element;
  }
}

[TestFixture]
public class PeekableTests {
  [TestCase("abc")]
  [TestCase("bcd")]
  public void Peek_ShouldReturnCorrectValue(string input) {
    var peekable = new StringPeekable(input);
    Assert.That(peekable.Peek(), Is.EqualTo(input[0]));
  }

  [TestCase("hello")]
  [TestCase("world")]
  public void Peek_ShouldReturnCorrectValueAfterNext(string input) {
    var peekable = new StringPeekable(input);
    peekable.Next();
    Assert.That(peekable.Peek(), Is.EqualTo(input[1]));
  }

  [Test]
  public void Peek_ShouldReturnSameElementWhenConsecutive() {
    var peekable = new StringPeekable("a");
    var firstPeek = peekable.Peek();
    var secondPeek = peekable.Peek();
    Assert.That(firstPeek, Is.EqualTo(secondPeek));
  }

  [TestCase("abc")]
  [TestCase("bcd")]
  public void Next_ShouldReturnCorrectValue(string input) {
    var peekable = new StringPeekable(input);
    Assert.That(peekable.Next(), Is.EqualTo(input[0]));
  }

  [TestCase("hello")]
  [TestCase("world")]
  public void Next_ShouldCorrectlyAdvanceForward(string input) {
    var peekable = new StringPeekable(input);
    foreach (var t in input) {
      Assert.That(peekable.Next(), Is.EqualTo(t));
    }
  }
}
