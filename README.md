# acNET

solved.ac API�� �ٷ�� ���� C# ���� ���̺귯���Դϴ�.<br><br>
solved.ac ����� API ������ �����߽��ϴ�.<br>
(��ó: https://github.com/solvedac/unofficial-documentation)

## ����
```cs
using acNET;

acAPI api = new();
var user = api.GetUser("jyunni");
Console.WriteLine("tier: {0}\nclass: {1}", user.GetTierName, user.@class);
```