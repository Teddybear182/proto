namespace Proto.Cli.Commands;

using System.ComponentModel;
using Proto.Cli.Utils;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("Launches language interactive shell.")]
internal sealed class ReplCommand : Command {
  private readonly List<string> _history = new List<string>();

  public override int Execute(CommandContext context) {
    PrintCompilerInformation();
    var isReplRunning = true;
    while (isReplRunning) {
      isReplRunning = this.PromptAndProcess();
    }
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

  private bool PromptAndProcess() {
    // TODO: this method should be changed in the future
    this.PrintReplInputHeader();
    var userInput = AnsiConsole.Prompt(
      new TextPrompt<string>("[orange1] >[/]")
    );
    if (userInput.Trim() == ":exit") {
      return false;
    }
    // TODO: process input
    this._history.Add(userInput);
    return true;
  }

  private void PrintReplInputHeader() {
    var index = this._history.Count;
    var headerElement = new Rule($"Prompt ({index})")
      .RuleStyle(Color.Blue)
      .Justify(Justify.Left);
    AnsiConsole.Write(headerElement);
  }
}
