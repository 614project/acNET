# acNET

solved.ac API wrapper library for C#. (unofficial)

## Example
```cs
using acNET;

acAPI api = new();
//sync
Console.Write(api.GetUser("jyunni").Result);
//async
(var info, Exception? ex) = await api.GetUserAsync("shiftpsh");
Console.Write(info);
```