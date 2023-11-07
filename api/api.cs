using acNET.Image;
using acNET.Type;
using acNET.User;
using System.Net;

namespace acNET
{

    /// <summary>
    /// solved.ac API 를 다루기 위한 클래스입니다.
    /// </summary>
    public class API : IDisposable
    {
        HttpClient client;

        const string BaseLink = "https://solved.ac/api/v3/";

        public API()
        {
            this.client = new() { BaseAddress = new(BaseLink) };
        }

        /// <summary>
        /// acNET.API 사용 도중 발생한 오류가 저장되는 큐입니다.
        /// </summary>
        public readonly Queue<Exception> Errors = new();

        /// <summary>
        /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
        /// </summary>
        /// <param name="handle">사용자 ID</param>
        /// <returns>실패시 null</returns>
        public UserInfo? User(string handle) => GET<UserInfo>("user/show", $"?handle={handle}");
        /// <summary>
        /// 사용자가 속한 조직 목록를 가져옵니다.
        /// </summary>
        /// <param name="handle">사용자 ID</param>
        /// <returns>실패시 null</returns>
        public Group? Group(string handle) => GET<Group>("user/organizations", $"?handle={handle}");
        /// <summary>
        /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
        /// </summary>
        /// <param name="handle">사용자 ID</param>
        /// <returns>실패시 null</returns>
        public UserSolvedInfo? UserSolvedInfo(string handle) => GET<UserSolvedInfo>("user/problem_stats", $"?handle={handle}");
        /// <summary>
        /// 배경의 정보를 가져옵니다.
        /// </summary>
        /// <param name="backgroundId">배경 ID</param>
        /// <returns>실패시 null</returns>
        public Background? Background(string backgroundId) => GET<Background>("background/show", $"?backgroundId={backgroundId}");

        internal T? GET<T>(string url, string option) where T : BaseBody
        {
            try
            {
                if (!this.Request(url, option, out string json)) return null;
                return Converter.ParsingJson<T>(json);
            } catch (Exception e)
            {
                this.Errors.Enqueue(e);
                return null;
            }
        }

        internal bool Request(string url,string option,out string content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new(BaseLink + url + option),
            };
            var response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
            if((int)response.StatusCode < 200 || (int)response.StatusCode > 299)
            {
                content = response.StatusCode.ToString();
                return false;
            }
            var responseInfo = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            content = responseInfo;
            return true;
        }

        internal static bool SaveImage(string url, string filename, out string fullname)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    int dot = url.LastIndexOf('.');
                    fullname = filename;
                    if (dot >= 0)
                    {
                        fullname += url.Substring(dot);
                    }
                    webClient.DownloadFile(url, fullname);
                }
                return true;
            } catch (Exception e)
            {
                fullname = e.Message;
                return false;
            }
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }


}
