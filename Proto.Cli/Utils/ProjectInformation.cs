namespace Proto.Cli.Utils;

using System.Reflection;

internal readonly record struct ProjectInformation {
  private const string UnknownPlaceholder = "unknown";

  public string Version { get; private init; }
  public string ProductName { get; private init; }
  public string Authors { get; private init; }
  public string Description { get; private init; }
  public string RepositoryUrl { get; private init; }

  public static ProjectInformation ReadProjectInformationFromAssembly() {
    return new ProjectInformation {
      Version = GetVersion(),
      ProductName = GetProductName(),
      Authors = GetAuthors(),
      Description = GetDescription(),
      RepositoryUrl = GetRepositoryUrl(),
    };
  }

  private static Assembly GetAssembly() {
    return Assembly.GetExecutingAssembly();
  }

  private static string GetVersion() {
    return GetAssembly()
      .GetName()
      .Version
      ?.ToString() ?? UnknownPlaceholder;
  }

  private static string GetProductName() {
    return GetAssembly()
      .GetCustomAttribute<AssemblyProductAttribute>()
      ?.Product ?? UnknownPlaceholder;
  }

  private static string GetAuthors() {
    return GetAssembly()
      .GetCustomAttribute<AssemblyCompanyAttribute>()
      ?.Company ?? UnknownPlaceholder;
  }

  private static string GetDescription() {
    return GetAssembly()
      .GetCustomAttribute<AssemblyDescriptionAttribute>()
      ?.Description ?? UnknownPlaceholder;
  }

  private static string GetRepositoryUrl() {
    return GetAssembly()
      .GetCustomAttributes<AssemblyMetadataAttribute>()
      .FirstOrDefault(attribute => attribute.Key == "RepositoryUrl")
      ?.Value ?? UnknownPlaceholder;
  }
}
