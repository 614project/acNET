# acNET

solved.ac API�� �ٷ�� ���� C# ���� ���̺귯���Դϴ�.<br><br>
[solved.ac ����� API ����]�� �����߽��ϴ�.](https://github.com/solvedac/unofficial-documentation)<br>

## ����
```cs
using acNET;

acAPI api = new();
var user = api.GetUser("jyunni");
Console.WriteLine("tier: {0}\nclass: {1}", user.GetTierName, user.@class);
```
## ���
�ڼ��� ������ ��Ű�� �������ּ���.

## �����
.NET 6.0 �̻� SDK �׸��� (���� ��ġ�� �ȵ��ִٸ�) nuget���� Newtonsoft.Json�� ��ġ�Ͻø� �˴ϴ�.<br>
�׽�Ʈ�� ���� ���� ������Ʈ�� �����ؼ� �� ������Ʈ�� �����Ͽ� ����Ͻø� �˴ϴ�.