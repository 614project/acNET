namespace AcNET;

/// <summary>
/// 이미지 등 파일을 내려받는 도우미입니다.
/// </summary>
internal static class SolvedFile
{
    static readonly Lazy<HttpClient> client = new(() => new HttpClient(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(5),
    }));

    internal static async Task<Exception?> SaveAsync(string? url, string savePath, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(url);
            ArgumentException.ThrowIfNullOrEmpty(savePath);
            using var response = await client.Value.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            await using Stream contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            await using FileStream fileStream = new(savePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            await contentStream.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            return ex;
        }
        return null;
    }
}
