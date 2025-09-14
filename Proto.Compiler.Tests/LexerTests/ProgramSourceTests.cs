namespace Proto.Compiler.Tests.LexerTests;

using Proto.Compiler.Lexer;
using Proto.Compiler.Utils;

[TestFixture]
public class ProgramSourceTests {
  #region Implementation Cases

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

  #endregion

  #region Location Cases

  [TestCase("while")]
  public void ProgramSource_ShouldHandleLocations_OneLine(string input) {
    AssertLocations(input, [
      (Line: 1, Column: 1, Offset: 0),
      (Line: 1, Column: 2, Offset: 1),
      (Line: 1, Column: 3, Offset: 2),
      (Line: 1, Column: 4, Offset: 3),
      (Line: 1, Column: 5, Offset: 4)
    ]);
  }

  [TestCase("a\n\tb")]
  public void ProgramSource_ShouldHandleLocations_MultiLine(string input) {
    AssertLocations(input, [
      (Line: 1, Column: 1, Offset: 0),
      (Line: 1, Column: 2, Offset: 1),
      (Line: 2, Column: 1, Offset: 2),
      (Line: 2, Column: 2, Offset: 3)
    ]);
  }

  #endregion

  #region Helper Methods

  private static void AssertLocations(string input, IEnumerable<(uint Line, uint Column, uint Offset)> expectedLocations) {
    var locations = ReadLocations(input).ToArray();
    var expected = expectedLocations.ToArray();
    Assert.Multiple(() => {
      Assert.That(locations, Has.Length.EqualTo(expected.Length));
      for (var i = 0; i < expected.Length; i++) {
        Assert.Multiple(() => {
          Assert.That(locations[i].Line, Is.EqualTo(expected[i].Line));
          Assert.That(locations[i].Column, Is.EqualTo(expected[i].Column));
          Assert.That(locations[i].Offset, Is.EqualTo(expected[i].Offset));
        });
      }
    });
  }

  private static List<Location> ReadLocations(string input) {
    var source = new ProgramSource(new StringReader(input));
    var locations = new List<Location>();
    while (!source.IsEof) {
      locations.Add(source.Location);
      source.Next();
    }
    return locations;
  }

  #endregion
}
