# acNET

solved.ac API를 다루기 위한 C# 래퍼 라이브러리입니다.<br><br>
[solved.ac 비공식 API 문서](https://github.com/solvedac/unofficial-documentation)를 참고했습니다.<br>

## 예제
```cs
using acNET;

acAPI api = new();
Console.Write(api.GetUser("jyunni"));
```
## 사용
깃허브 리포지토리에서 Packages 항목에 있는 최신 버전의 nuget을 추가하여 사용하시면 됩니다!<br>
(가능한 최신 버전을 사용해주세요!)<br>
자세한 사용법은 위키를 참고해주세요.

## 디버깅
.NET 6.0 이상 SDK 그리고 (만약 설치가 안되있다면) nuget에서 [RestSharp](https://github.com/restsharp/RestSharp)을 설치하시면 됩니다.<br>
테스트시 따로 시작 프로젝트를 생성해서 이 프로젝트를 참조하여 사용하시면 됩니다.

# 기본적인 예제
acNET.acAPI 변수를 생성하고 사용하면 됩니다.  

대부분 함수/변수에 설명을 달아놓았으므로 참고하여 사용하면 되겠습니다. (c# summary)
## 동기
```cs
using acNET;

acAPI api = new();
(var stats, Exception? ex) = api.GetSiteStats();

if (ex is Exception e)
    Console.Write("예외: {0}",e);
else
    Console.Write("솔브닥 가입자수: {0}", stats.userCount);
```
(0.8 이상에서만 작동하는 예제입니다.)
## 비동기
동기와 크게 다르지 않습니다. 맨 뒤에 'Async' 문자열이 포함된 함수는 모두 비동기 함수입니다.
```cs
using acNET;

acAPI api = new();
(var user, Exception? ex) = await api.GetUserAsync("shiftpsh");

if (ex is Exception e)
    Console.Write("예외: {0}",e);
else
    Console.Write("{0}님의 티어: {1}(레이팅: {2})",user.handle, user.GetTierName, user.rating);
```
개인적으로 비동기 방식을 성능 면에서 권장합니다.

# 예외 처리
## acAPIError
``class acAPIError : Exception`` 이 클래스는 응답에 실패했을때 발생하는 예외입니다.  
``acAPIError.Code``에 http 응답코드가 포함되어 있습니다.  
  
만약 예외가 ``acAPIError``일 경우 응답에 실패한것으로 간주하면 됩니다.  
그렇지 않을경우, Json 파싱에 실패하는 등의 `개발자 조차 예측하지 못한 예외입니다. 이슈 남겨주세요!
## 0.8.x
모든 요청은 ``acResult<T>(T Result,Exception? Exception)`` 레코드 형식으로 반환됩니다.  
(비동기도 ``async Task<acResult>``로 반환됩니다.)  
```cs
acNET.acAPI api = new();

var user = api.GetUser("jyunni").Result;
(RankedUser? result, Exception? ex) = await api.GetUserAsync("jyunni");
```
``Result``가 ``null``이라면 요청이 실패한 것으로  ``Exception != null``을 보장합니다.  
``Exception``이 null이라면 요청이 성공한 것으로 ``Result != null``을 보장합니다.  
## 0.7.2 이전
``acNET.acAPI``내 모든 매서드는 명령을 처리하는 도중 예외가 발생하면, 해당 예외가 ``acAPI.Errors`` 큐에 삽입되고 중단됩니다. (즉,``acAPI``를 사용하실 땐 try-catch문을 사용하실 필요가 없습니다.)<br>
이를 통해 아레와 같이 예외가 발생한 이유를 출력할수 있습니다.
```cs
acNET.acAPI api = new();
api.GetUser("(대충에러를띄우기위한문자열)");

if (api.Errors.Count > 0)
    Console.WriteLine(api.Errors.Dequeue());

//0.7 에서만
api.GetUser("(존재하지않는핸들)",out acAPIError? error);
if (error is not null)
    Console.WriteLine(error);
```
0.7.2에 유일하게 추가된 비동기 함수는, 0.8과 유사하게 작동합니다. 위 문단을 참고하세요.
