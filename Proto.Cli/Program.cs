namespace Proto.Cli;

using Proto.Cli.Commands;
using Spectre.Console.Cli;

internal static class Program {
  public static int Main(string[] args) {
    var app = new CommandApp();
    app.Configure(config => {
      config.AddCommand<CompileCommand>("compile");
      config.AddCommand<ReplCommand>("repl");
      config.AddCommand<RunCommand>("run");
      config.AddCommand<AboutCommand>("about");
    });
    return app.Run(args);
  }
}
