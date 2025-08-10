namespace Proto.Cli.Commands;

using System.ComponentModel;
using Proto.Cli.Utils;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("Launches language interactive shell.")]
internal sealed class ReplCommand : Command {
  public override int Execute(CommandContext context) {
    PrintCompilerInformation();
    return 0;
  }

  private static void PrintCompilerInformation() {
    var compilerInfo = CompilerInformation.ReadCompilerInformationFromAssembly();
    var compilerNameText = new FigletText(compilerInfo.CompilerName)
      .Color(Color.Purple);
    var compilerVersionText = new Panel($"Language version: [blue]{compilerInfo.CompilerVersion}[/]")
      .Padding(1, 1)
      .Border(BoxBorder.None);
    AnsiConsole.Write(compilerNameText);
    AnsiConsole.Write(compilerVersionText);
  }
}
