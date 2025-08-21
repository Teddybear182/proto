namespace Proto.Compiler.Tests.UtilsTests;

using Proto.Compiler.Utils;

[TestFixture]
public class LocationTests {
  [TestCase(1, 1)]
  [TestCase(2, 5)]
  [TestCase(5, 10)]
  public void ToString_ShouldContainLine(int line, int column) {
    var location = new Location((uint) line, (uint) column);
    var locString = location.ToString();
    Assert.That(locString, Contains.Substring(line.ToString()));
  }

  [TestCase(10, 20)]
  [TestCase(121, 576)]
  public void ToString_ShouldContainColumn(int line, int column) {
    var location = new Location((uint) line, (uint) column);
    var locString = location.ToString();
    Assert.That(locString, Contains.Substring(column.ToString()));
  }
}
