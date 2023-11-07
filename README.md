# acNET

solved.ac API를 다루기 위한 C# 래퍼 라이브러리입니다.<br><br>
solved.ac 비공식 API 문서를 참고했습니다.<br>
(출처: https://github.com/solvedac/unofficial-documentation)

## 예제
```cs
using acNET;

acAPI api = new();
var user = api.GetUser("jyunni");
Console.WriteLine("tier: {0}\nclass: {1}", user.GetTierName, user.@class);
```