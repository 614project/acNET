# acNET
[![](https://img.shields.io/nuget/v/acNET)](https://www.nuget.org/packages/acNET)

## Introduce
A C# wrapper library for interacting with the solved.ac API (v4).<br><br>
Refer to the [solved.ac API documentation](https://backstage.solved.ac/developer/docs).<br>
Register an application on [solved.ac Backstage](https://backstage.solved.ac/developer) to get a client ID and secret.

## Example
```cs
using AcNET;

using SolvedAPI api = new("CLIENT_ID", "CLIENT_SECRET");
var user = await api.GetUserAsync("jyunni");
Console.WriteLine(user.IsSuccess ? user.Result!.Rating : user.Exception);
```
## Usage
This is a .NET 10 library. All APIs are asynchronous and return `SolvedResult<T>`;
errors from solved.ac are exposed as `SolvedAPIException` (`ErrorCode`, `ErrorMessage`, `ErrorDetails`).

For more detailed usage, please refer to the README and wiki of the GitHub repository!
