## Proto: prototype programming language

This project aims to create a programming language
based on top of the .NET platform.

### Proto.Compiler

Library responsible for compiler logic, e.g.
for `code generation`.

### Proto.Cli

Command-line interface using `Proto.Compiler` to
compile files and run code.

### Devtools

#### Formatting

This project contains `.editorconfig` file. This
file contains formatting and linting rules. To format
the whole project, run:

```bash
dotnet format
```

> Most IDEs support `.editorconfig` file
> by default and will apply those rules automatically

#### Dependencies

This project contains some dependencies in the
`Directory.Build.props` file. To install them, run:

```bash
dotnet restore
```

Currently, `Sonar` analyzer is enabled for this project.
To check Sonar warnings, run:

```bash
dotnet build
```
