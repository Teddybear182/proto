namespace Proto.Cli.Utils;

using System.Reflection;

internal readonly record struct CompilerInformation {
  private const string CompilerAssemblyName = "Proto.Compiler";
  private const string UnknownPlaceholder = "unknown";

  public string CompilerName { get; private init; }
  public string CompilerVersion { get; private init; }

  public static CompilerInformation ReadCompilerInformationFromAssembly() {
    var compilerAssembly = Assembly.Load(CompilerAssemblyName);
    var compilerProductName = compilerAssembly
      .GetCustomAttribute<AssemblyProductAttribute>()
      ?.Product;
    var compilerAssemblyName = compilerAssembly
      .GetName()
      .Name;
    var compilerName = compilerProductName ?? compilerAssemblyName ?? UnknownPlaceholder;
    var compilerVersion = compilerAssembly
      .GetName()
      .Version
      ?.ToString() ?? UnknownPlaceholder;

    return new CompilerInformation {
      CompilerName = compilerName,
      CompilerVersion = compilerVersion,
    };
  }
}
