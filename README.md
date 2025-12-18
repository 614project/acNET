# acNET
[![](https://img.shields.io/nuget/v/acNET)](https://www.nuget.org/packages/acNET)

[한국어(Korean)](./README-KR.md)

## Introduce
A C# wrapper library for interacting with the solved.ac API.<br><br>
Refer to the [solved.ac unofficial API documentation](https://github.com/solvedac/unofficial-documentation).<br>

## Example
```cs
using AcNET;

SolvedAPI api = new();
Console.Write(api.GetUser("jyunni").Result.OverRating);
```
## Usage
This is a .NET Standard 2.1 library.  
That means you can use it in any project targeting .NET 5 (C# 9.0) or higher  
by simply adding the latest version from NuGet!

For more detailed usage, please refer to the wiki!

## Debugging
All you need is the .NET 8.0 SDK!  
For testing, you can create a separate startup project and reference this project for use.
