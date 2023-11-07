using acNET.Type;
using System.Net;

namespace acNET
{
    public partial class acAPI
    {
        internal T? GET<T>(string url, string? option=null) where T : BaseBody
        {
            try
            {
                if (!this.Request(url, option??string.Empty, out string json)) return null;
                return Converter.ParsingJson<T>(json);
            }
            catch (Exception e)
            {
                this.Errors.Enqueue(e);
                return null;
            }
        }

        internal List<T>? GETLIST<T>(string url, string? option = null) where T : BaseBody
        {
            try
            {
                if (!this.Request(url, option ?? string.Empty, out string json)) return null;
                return Converter.ParsingJsonList<T>(json);
            }
            catch (Exception e)
            {
                this.Errors.Enqueue(e);
                return null;
            }
        }

        internal bool Request(string url, string option, out string content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new(APIurl + url + option),
            };
            var response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
            if ((int)response.StatusCode < 200 || (int)response.StatusCode > 299)
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
            }
            catch (Exception e)
            {
                fullname = e.Message;
                return false;
            }
        }

    }
}
