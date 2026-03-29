# HeroWarsUtils Development Guidelines

## Build Commands

```bash
# Build the entire solution
dotnet build

# Build specific project
dotnet build src/StatisticsManagement/StatisticsManagement.csproj
dotnet build src/StatisticsFormApp/StatisticsFormApp.csproj
```

## Test Commands

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run a single test by name
dotnet test --filter "FullyQualifiedName~Convert_EmptyCsv_ReturnsEmptyTable"

# Run tests in a specific class
dotnet test --filter "FullyQualifiedName~CsvToInputTableConverterTests"

# Run tests in a specific namespace
dotnet test --filter "FullyQualifiedName~StatisticsManagement.Tests"
```

## Code Style Guidelines

### Imports

- Place `using` directives in the following order:
  1. Project-specific namespaces (e.g., `StatisticsManagement.*`)
  2. `System` namespaces
  3. Third-party namespaces
- Sort imports alphabetically within each group
- Use explicit imports; avoid `static` imports except in test files
- Include `System.Text` and `System.Collections.Generic` where needed

### Formatting

- Use 4 spaces for indentation (no tabs)
- Method body opening brace on the same line
- Empty lines between logical blocks
- Line length limit: 120 characters
- Use C# 10+ features: collection expressions (`[]`), target-typed `new`, primary constructors
- Prefer `var` for obvious types; use explicit types for complex types

### Types

- Use `class` for mutable objects with identity
- Use `struct` for value types with 2-4 fields, immutable semantics
- Use `record` for immutable DTOs and data carriers (not yet adopted in this codebase)
- Mark DTO classes as `internal` unless library API
- Use `internal enum` for domain-specific enumerations
- Prefer `List<T>` over array for collections
- Use properties with auto-getter only (`{ get; }`) for immutable properties

### Naming Conventions

- **Classes**: PascalCase, descriptive nouns (`StatisticsManager`, `CsvToInputTableConverter`)
- **Interfaces**: Prefix with `I`, PascalCase (e.g., `IStatisticsService`)
- **Methods**: PascalCase, verb-noun pattern (`Convert`, `Process`, `ConvertToEnum`)
- **Properties**: PascalCase (`DailyActivity`, `InputPlayers`)
- **Fields**: camelCase with underscore prefix (`_converter`, `_russianToEnum`)
- **Constants**: UPPERCASE_WITH_UNDERSCORES (not yet used)
- **Private methods**: camelCase (if any exposed)
- **Test classes**: Suffix with `Tests` (`CsvToInputTableConverterTests`)
- **Test methods**: `MethodUnderTest_Scenario_ExpectedResult` (e.g., `Convert_EmptyCsv_ReturnsEmptyTable`)

### Single Return Statement Rule

**Every method must have exactly one return statement, and it must be the last line of the method.**

- Use early validation with `throw` statements for guard conditions
- Use `if/else` blocks to assign to a single result variable
- Return the result variable as the final statement only
- For void methods, the single "return" is the implicit end; no early `return;` statements

**Incorrect:**
```csharp
public double Calculate(double value)
{
    if (value < 0)
        return 0;
    
    return value * 2;
}
```

**Correct:**
```csharp
public double Calculate(double value)
{
    double result;
    
    if (value < 0)
    {
        result = 0;
    }
    else
    {
        result = value * 2;
    }
    
    return result;
}
```

### Error Handling

- Throw specific exceptions: `ArgumentException`, `ArgumentNullException`, `FormatException`
- Validate inputs at method boundaries (guard clauses)
- Do not catch exceptions unless you can recover or wrap with domain context
- Include parameter name in `ArgumentException` constructor
- Use `TryParse` or `TryGetValue` patterns for non-exceptional flow parsing

**Example:**
```csharp
public DayOfWeekEnum ConvertToEnum(string str)
{
    if (string.IsNullOrWhiteSpace(str))
    {
        throw new ArgumentException("Input string cannot be empty", nameof(str));
    }

    if (!_russianToEnum.TryGetValue(str, out DayOfWeekEnum result))
    {
        throw new ArgumentException($"Invalid day of week string: {str}");
    }

    return result;
}
```

### Tests

- Use xUnit framework with `[Fact]` attribute
- Follow Arrange-Act-Assert pattern with comments
- Use descriptive test names: `Method_Scenario_ExpectedResult`
- Test both success and failure cases
- Test edge cases: empty input, null, whitespace, invalid data
- Use `_camelCase` for test class fields (fixtures)

### Comments

- Russian comments for business logic explanations
- Avoid comments explaining code; code should be self-documenting
- Use XML docs for public API members if needed

### Nullable Reference Types

- Enable `Nullable` (project setting: `<Nullable>enable</Nullable>`)
- Use `!` only when explicitly asserting non-null in tests or interop
- Prefer optional parameters or nullable types over default value hacks

### Architecture

- Separate input models (`InputModels`) from calculation models (`CalculationModels`)
- Use converter pattern for transformations between model types
- Mark converters as `internal` with `InternalsVisibleTo` for tests
- Keep calculation logic in `StatisticsManager` or dedicated services
