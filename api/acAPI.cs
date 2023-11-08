using System.Runtime.CompilerServices;

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

    /// <summary>
    /// acAPI가 자체적으로 일으킨 의도적인 예외 (C# 런타임 에러가 아닙니다.)
    /// </summary>
    public class acAPIError : Exception
    {
        public acAPIError() { }
        public acAPIError(string message,short code=-1) : base(message) { this.Code = code; }
        public acAPIError(string message, Exception inner) : base(message, inner) { }
        protected acAPIError(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public static acAPIError Create(string message,short code=-1)
        {
            acAPIError error = new acAPIError(message,code);
            return error;
        }

        /// <summary>
        /// 오류 코드입니다. (0~599: API 응답 코드, -1: 알수 없음, 이외: 위키 참고)
        /// </summary>
        public short Code { get; init; }
    }
}
