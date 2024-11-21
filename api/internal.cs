using Newtonsoft.Json;

namespace acNET;

public partial class acAPI
{
    internal acResult<T> Get<T>(string url, string? option = null)
    {
        return GetAsync<T>(url , option).GetAwaiter().GetResult();
    }
    internal async Task<acResult<T>>GetAsync<T>(string url, string? option = null)
    {
        try
        {
            var ret = await _client.GetAsync(url + option ?? string.Empty);
            int code = (int)ret.StatusCode;
            return new(JsonConvert.DeserializeObject<T>(await ret.Content.ReadAsStringAsync()) , null);
        }
        catch (Exception ex)
        {
            return new(default , ex);
        }
    }
    internal static async Task<Exception?> SaveFile(string url,string savePath)
    {
        try
        {
            using HttpClient client = new();
            using var response = await client.GetAsync(url , HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using Stream contentStream = await response.Content.ReadAsStreamAsync();
            using FileStream fileStream = new(savePath , FileMode.Create , FileAccess.Write , FileShare.None , 8192 , true);
            await contentStream.CopyToAsync(fileStream);
        }
        catch (Exception ex)
        {
            return ex;
        }
        return null;
    }
}
