namespace AcNET;

internal static class SolvedOnceAPI
{
    internal static async ValueTask<Exception?> SaveFile(string url , string savePath)
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

    internal static SolvedResult<T> Once<T>(Func<SolvedAPI, SolvedResult<T>> func)
    {
        using SolvedAPI api = new();
        return func(api);
    }
}
