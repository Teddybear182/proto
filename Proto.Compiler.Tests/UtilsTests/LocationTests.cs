namespace Proto.Compiler.Tests.UtilsTests;

using Proto.Compiler.Utils;

[TestFixture]
public class LocationTests {
  // we don't test offset since it's not included
  // in the ToString method
  private const uint FallbackOffset = 0;

  [TestCase(1, 1)]
  [TestCase(2, 5)]
  [TestCase(5, 10)]
  public void ToString_ShouldContainLine(int line, int column) {
    var location = new Location((uint) line, (uint) column, FallbackOffset);
    var locString = location.ToString();
    Assert.That(locString, Contains.Substring(line.ToString()));
  }

  [TestCase(10, 20)]
  [TestCase(121, 576)]
  public void ToString_ShouldContainColumn(int line, int column) {
    var location = new Location((uint) line, (uint) column, FallbackOffset);
    var locString = location.ToString();
    Assert.That(locString, Contains.Substring(column.ToString()));
  }
}
