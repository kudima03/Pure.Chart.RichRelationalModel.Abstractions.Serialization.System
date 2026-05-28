# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet build --no-restore -warnaserror
dotnet format --verify-no-changes             # check code style (CI enforces this)
dotnet format                                  # auto-fix code style
dotnet test --no-build --verbosity normal      # run xUnit tests
dotnet pack --configuration Release -p:PackageVersion=<version> --output .
```

## Architecture

This is a **converter library** — it provides `System.Text.Json` converters for the abstract chart interfaces from `Pure.Chart.RichRelationalModel.Abstractions`.

**Public surface:**
- `ChartRichRelationalModelAbstractionsConverters` — implements `IEnumerable<JsonConverter>`; yields all four converters and is the intended entry point for registering them with `JsonSerializerOptions`.
- `ChartRichRelationalModelConverter` — `JsonConverter<IChartRichRelationalModel>`
- `ChartTypeRichRelationalModelConverter` — `JsonConverter<IChartTypeRichRelationalModel>`
- `AxisRichRelationalModelConverter` — `JsonConverter<IAxisRichRelationalModel>`
- `ChartSeriesRichRelationalModelConverter` — `JsonConverter<IChartSeriesRichRelationalModel>`

Each converter uses an internal `sealed record` (e.g. `ChartRichRelationalModelJsonModel`) that implements the corresponding interface and is decorated with `[JsonConstructor]`. The `Read` method deserializes into the internal record; `Write` wraps the incoming interface value in the internal record before serializing, ensuring the full graph is always written with the expected shape.

**`IChartRichRelationalModel` inherits from both `IChart` and `IChartRelationalModel`**, creating a diamond hierarchy. The internal record explicitly implements `IChart`'s covariant members (e.g. `IChartType IChart.Type`) to resolve the ambiguity — keep this pattern when adding new converters.

**Multi-targeting:** net7.0, net8.0, net9.0, net10.0. The library is **not** AOT-compatible (`IsAotCompatible` is not set).

**Package validation:** `EnablePackageValidation = true` with `PackageValidationBaselineVersion = 0.1.0-preview.2.0.0`. Breaking API changes fail the build.

**Publishing:** triggered by pushing a semver tag (`*.*.*`). The tag value becomes `PackageVersion`.

**Tests:** The test project (`net10.0`) uses xUnit with `coverlet` for coverage. CI enforces 99 % line coverage and 99 % mutation score (dotnet-stryker).

## Code Style

Enforced via `.editorconfig` and `dotnet format --verify-no-changes` in CI. Non-obvious rules:

- No `var` — always use explicit types.
- No expression-bodied methods or constructors; expression-bodied properties and accessors are required.
- `using` directives go outside the namespace.
- File-scoped namespace declarations (`namespace Foo;`).
- Private fields: `_camelCase`. No non-private instance fields.
- Max line length: 90 characters.
- `new` without explicit type (`new()`) is disallowed even when the type is apparent.
- All braces on their own line (`csharp_new_line_before_open_brace = all`).
- `System.*` using directives sorted before other directives; no blank line between using groups.

## Commit Messages

Do not mention Claude or AI assistance in commit messages.
