# Delta.Tests

Unit test project for the Delta application.

## Running Tests

To run all tests:
```bash
dotnet test
```

To run tests with coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Test Structure

- `Controllers/Common/` - Tests for Common controllers
  - `CommonSearchControllerTests.cs` - Unit tests for CommonSearchController

## Dependencies

- xUnit - Testing framework
- Moq - Mocking framework
- Microsoft.AspNetCore.Mvc.Testing - ASP.NET Core testing utilities