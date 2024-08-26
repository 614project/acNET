# acNET

**이제 acNET을 Nuget에서 사용하실수 있습니다!**
[![](https://img.shields.io/nuget/v/acNET)](https://www.nuget.org/packages/acNET)

solved.ac API를 다루기 위한 C# 래퍼 라이브러리입니다.<br><br>
[solved.ac 비공식 API 문서](https://github.com/solvedac/unofficial-documentation)를 참고했습니다.<br>

## 예제
```cs
using acNET;

acAPI api = new();
Console.Write(api.GetUser("jyunni").Result);
```
## 사용
최신 버전의 Nuget을 추가하여 사용하시면 됩니다!<br>
(가능한 최신 버전을 사용해주세요!)<br>
자세한 사용법은 위키를 참고해주세요.

## 디버깅
.NET 6.0 이상 SDK 그리고 (만약 설치가 안되있다면) nuget에서 [RestSharp](https://github.com/restsharp/RestSharp)을 설치하시면 됩니다.<br>
테스트시 따로 시작 프로젝트를 생성해서 이 프로젝트를 참조하여 사용하시면 됩니다.
