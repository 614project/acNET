# acNET
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
.NET standard 2.1 라이브러리입니다.  
즉, .NET 5 (C# 9.0) 이상의 모든 프로젝트에서
최신 버전의 Nuget을 추가하여 사용하시면 됩니다!  

자세한 사용법은 위키를 참고해주세요!

## 디버깅
.NET 8.0 이상 SDK만 있으면 됩니다!  
테스트시 따로 시작 프로젝트를 생성해서 이 프로젝트를 참조하여 사용하시면 됩니다.
