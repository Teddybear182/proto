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

This project contains `.editorconfig` file. This
file contains formatting and linting rules. To format
the whole project, run:

```bash
dotnet format
```

> Most IDEs support `.editorconfig` file
> by default and will apply those rules automatically
