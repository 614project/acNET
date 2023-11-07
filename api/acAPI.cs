namespace acNET
{
    /// <summary>
    /// solved.ac API 를 다룰수 있는 객체입니다.
    /// </summary>
    public partial class acAPI : IDisposable
    {
        HttpClient client;

        /// <summary>
        /// solved.ac api 기본 주소.
        /// </summary>
        public const string APIurl = "https://solved.ac/api/v3/";

        public acAPI()
        {
            this.client = new() { BaseAddress = new(APIurl) };
        }

        /// <summary>
        /// acNET.API 사용 도중 발생한 오류가 저장되는 큐입니다.
        /// </summary>
        public readonly Queue<Exception> Errors = new();

        public void Dispose()
        {
            this.client.Dispose();
        }
    }

}
