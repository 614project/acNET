namespace acNET;

/// <summary>
/// solved.ac API 를 다룰수 있는 객체입니다.
/// </summary>
public partial class acAPI : IDisposable
{
    readonly HttpClient _client;
    /// <summary>
    /// solved.ac api 기본 주소.
    /// </summary>
    public const string DefaultUrl = "https://solved.ac/api/v3/";
    /// <summary>
    /// acAPI 생성.
    /// </summary>
    /// <param name="url">solved.ac API 링크</param>
    public acAPI(string url = DefaultUrl)
    {
        this._client = new() {
            BaseAddress = new(url)
        };
    }
    /// <summary>
    /// acAPI 리소스를 정리합니다.
    /// </summary>
    public void Dispose()
    {
        this._client.Dispose();
        GC.SuppressFinalize(this);
    }
}