
namespace Proto.Cli.Commands;

using System.ComponentModel;
using Proto.Cli.Utils;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("Shows general information about the language and the CLI tool.")]
internal sealed class AboutCommand : Command {
  public override int Execute(CommandContext context) {
    var projectInfo = ProjectInformation.ReadProjectInformationFromAssembly();

    var productNameText = new FigletText(projectInfo.ProductName)
      .Color(Color.Purple);
    var versionAndAuthorsText = new Panel($"[blue]Version: {projectInfo.Version}, by {projectInfo.Authors}[/]")
      .Border(BoxBorder.Ascii)
      .BorderColor(Color.Blue);
    var descriptionText = new Panel(projectInfo.Description)
      .Padding(2, 2)
      .Border(BoxBorder.None);
    var repositoryUrlText = new Panel($"[blue]Repository: [underline][bold]{projectInfo.RepositoryUrl}[/][/][/]")
      .Border(BoxBorder.Ascii)
      .BorderColor(Color.Blue);

    AnsiConsole.Write(productNameText);
    AnsiConsole.Write(versionAndAuthorsText);
    AnsiConsole.Write(descriptionText);
    AnsiConsole.Write(repositoryUrlText);
    return 0;
  }
}
