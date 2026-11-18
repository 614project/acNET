using AcNET.Bookmark;
using static AcNET.SolvedQuery;

namespace AcNET;

public partial class SolvedAPI
{
    #region Bookmarks
    /// <summary>
    /// 모든 사용자의 공개 북마크 폴더를 가져옵니다. (<c>GET /bookmarks</c>)
    /// </summary>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="sort">정렬 기준</param>
    /// <param name="query">검색어</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFolderPage>> GetBookmarkFoldersAsync(int page = 1, SolvedBookmarkFolderSort sort = SolvedBookmarkFolderSort.Updated, string? query = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedBookmarkFolderPage>("bookmarks", new SolvedQuery().Add("page", page).Add("sort", sort).Add("q", query), cancellationToken);

    /// <summary>
    /// 사용자의 북마크 폴더를 가져옵니다. 본인이면 비공개 폴더도 포함됩니다. (<c>GET /users/{handle}/bookmarks</c>)
    /// </summary>
    /// <remarks>비공개 폴더까지 보려면 본인의 사용자 토큰이 필요합니다. (scope: <c>bookmarks</c>)</remarks>
    /// <param name="handle">사용자명</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFolderPage>> GetUserBookmarkFoldersAsync(string handle, int page = 1, CancellationToken cancellationToken = default)
        => GetAsync<SolvedBookmarkFolderPage>($"users/{Segment(handle)}/bookmarks", new SolvedQuery().Add("page", page), cancellationToken);

    /// <summary>
    /// 북마크 폴더 하나와 그 문제 한 페이지를 가져옵니다. (<c>GET /users/{handle}/bookmarks/{slug}</c>)
    /// </summary>
    /// <remarks>비공개 폴더는 본인의 사용자 토큰이 필요합니다. (scope: <c>bookmarks</c>)</remarks>
    /// <param name="handle">폴더 주인의 사용자명</param>
    /// <param name="slug">폴더의 slug</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="sort">문제 정렬 기준</param>
    /// <param name="direction">정렬 방향</param>
    /// <param name="query">폴더 안에서의 검색 쿼리</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFolderDetail>> GetUserBookmarkFolderAsync(
        string handle,
        string slug,
        int page = 1,
        SolvedBookmarkProblemSort sort = SolvedBookmarkProblemSort.Order,
        SolvedSortDirection direction = SolvedSortDirection.Ascending,
        string? query = null,
        CancellationToken cancellationToken = default)
        => GetAsync<SolvedBookmarkFolderDetail>(
            $"users/{Segment(handle)}/bookmarks/{Segment(slug)}",
            new SolvedQuery().Add("page", page).Add("sort", sort).Add("direction", direction).Add("q", query),
            cancellationToken);

    /// <summary>
    /// 요청한 사용자의 모든 북마크 폴더를 가져옵니다. (<c>GET /account/bookmarks</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks</c>)</remarks>
    /// <param name="problemId">지정하면 각 폴더에 이 문제가 들어있는지(<see cref="SolvedAccountBookmarkFolder.ContainsProblem"/>) 함께 알려줍니다.</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>최신순 폴더 목록</returns>
    public Task<SolvedResult<List<SolvedAccountBookmarkFolder>>> GetAccountBookmarkFoldersAsync(int? problemId = null, CancellationToken cancellationToken = default)
        => GetAsync<FoldersEnvelope<SolvedAccountBookmarkFolder>, List<SolvedAccountBookmarkFolder>>(
            "account/bookmarks",
            new SolvedQuery().Add("problemId", problemId),
            static e => e.Folders,
            cancellationToken);

    /// <summary>
    /// 북마크 폴더를 만듭니다. (<c>POST /account/bookmarks</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks.write</c>). slug 가 이미 있으면 409, 폴더가 너무 많으면 422 에러입니다.</remarks>
    /// <param name="folder">만들 폴더</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>만든 폴더</returns>
    public Task<SolvedResult<SolvedBookmarkFolder>> CreateBookmarkFolderAsync(SolvedBookmarkFolderCreate folder, CancellationToken cancellationToken = default)
    {
        Guard.NotNull(folder);
        return SendJsonAsync<SolvedBookmarkFolder>(HttpMethod.Post, "account/bookmarks", folder, cancellationToken);
    }

    /// <summary>
    /// 북마크 폴더의 slug, 이름, 설명, 색, 공개 범위를 바꿉니다. (<c>PATCH /account/bookmarks/{slug}</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks.write</c>)</remarks>
    /// <param name="slug">바꿀 폴더의 slug</param>
    /// <param name="update">바꿀 내용. null 인 항목은 바뀌지 않습니다.</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>바뀐 폴더</returns>
    public Task<SolvedResult<SolvedBookmarkFolder>> UpdateBookmarkFolderAsync(string slug, SolvedBookmarkFolderUpdate update, CancellationToken cancellationToken = default)
    {
        Guard.NotNull(update);
        return SendJsonAsync<SolvedBookmarkFolder>(HttpMethod.Patch, $"account/bookmarks/{Segment(slug)}", update, cancellationToken);
    }

    /// <summary>
    /// 북마크 폴더와 그 목록을 지웁니다. (<c>DELETE /account/bookmarks/{slug}</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks.write</c>)</remarks>
    /// <param name="slug">지울 폴더의 slug</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>성공시 true</returns>
    public Task<SolvedResult<bool>> DeleteBookmarkFolderAsync(string slug, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Delete, $"account/bookmarks/{Segment(slug)}", null, cancellationToken);

    /// <summary>
    /// 북마크 폴더 끝에 문제들을 넣습니다. 이미 있는 문제는 건너뜁니다. (<c>POST /account/bookmarks/{slug}/problems</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks.write</c>). 폴더가 너무 커지면 422 에러입니다.</remarks>
    /// <param name="slug">폴더의 slug</param>
    /// <param name="problemIds">넣을 문제 번호들</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFolderChange>> AddBookmarkProblemsAsync(string slug, IEnumerable<int> problemIds, CancellationToken cancellationToken = default)
    {
        Guard.NotNull(problemIds);
        return SendJsonAsync<SolvedBookmarkFolderChange>(HttpMethod.Post, $"account/bookmarks/{Segment(slug)}/problems", new ProblemIdsBody(problemIds.ToList()), cancellationToken);
    }

    /// <summary>
    /// 북마크 폴더에서 문제들을 뺍니다. (<c>POST /account/bookmarks/{slug}/problems/remove</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks.write</c>)</remarks>
    /// <param name="slug">폴더의 slug</param>
    /// <param name="problemIds">뺄 문제 번호들</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFolderChange>> RemoveBookmarkProblemsAsync(string slug, IEnumerable<int> problemIds, CancellationToken cancellationToken = default)
    {
        Guard.NotNull(problemIds);
        return SendJsonAsync<SolvedBookmarkFolderChange>(HttpMethod.Post, $"account/bookmarks/{Segment(slug)}/problems/remove", new ProblemIdsBody(problemIds.ToList()), cancellationToken);
    }

    /// <summary>
    /// 폴더 안 문제의 위치를 옮기거나 메모를 바꿉니다. (<c>PATCH /account/bookmarks/{slug}/problems/{problemId}</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks.write</c>)</remarks>
    /// <param name="slug">폴더의 slug</param>
    /// <param name="problemId">문제 번호</param>
    /// <param name="update">바꿀 내용</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFolderChange>> UpdateBookmarkProblemAsync(string slug, int problemId, SolvedBookmarkProblemUpdate update, CancellationToken cancellationToken = default)
    {
        Guard.NotNull(update);
        return SendJsonAsync<SolvedBookmarkFolderChange>(HttpMethod.Patch, $"account/bookmarks/{Segment(slug)}/problems/{Segment(problemId)}", update, cancellationToken);
    }

    /// <summary>
    /// 요청한 사용자의 북마크 폴더 순서를 바꿉니다. (<c>POST /account/bookmarks/order</c>)
    /// </summary>
    /// <remarks>
    /// 사용자 토큰 필요 (scope: <c>bookmarks.write</c>).
    /// 보낸 slug 목록이 지금 폴더 목록과 정확히 같지 않으면 <see cref="SolvedErrorCode.StaleOrder"/> 에러입니다.
    /// </remarks>
    /// <param name="slugs">새 순서의 모든 폴더 slug</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>성공시 true</returns>
    public Task<SolvedResult<bool>> ReorderBookmarkFoldersAsync(IEnumerable<string> slugs, CancellationToken cancellationToken = default)
    {
        Guard.NotNull(slugs);
        return SendNoContentAsync(HttpMethod.Post, "account/bookmarks/order", new SlugsBody(slugs.ToList()), cancellationToken);
    }

    /// <summary>
    /// 요청한 사용자가 즐겨찾기한 다른 사람의 폴더를 가져옵니다. (<c>GET /account/bookmark-favorites</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks</c>)</remarks>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFolderPage>> GetBookmarkFavoritesAsync(int page = 1, CancellationToken cancellationToken = default)
        => GetAsync<SolvedBookmarkFolderPage>("account/bookmark-favorites", new SolvedQuery().Add("page", page), cancellationToken);

    /// <summary>
    /// 다른 사람의 폴더를 즐겨찾기에 넣습니다. 이미 있어도 성공입니다. (<c>POST /account/bookmark-favorites/{handle}/{slug}</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks.write</c>)</remarks>
    /// <param name="handle">폴더 주인의 사용자명</param>
    /// <param name="slug">폴더의 slug</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFavorite>> AddBookmarkFavoriteAsync(string handle, string slug, CancellationToken cancellationToken = default)
        => SendJsonAsync<SolvedBookmarkFavorite>(HttpMethod.Post, $"account/bookmark-favorites/{Segment(handle)}/{Segment(slug)}", null, cancellationToken);

    /// <summary>
    /// 폴더를 즐겨찾기에서 뺍니다. (<c>DELETE /account/bookmark-favorites/{handle}/{slug}</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>bookmarks.write</c>)</remarks>
    /// <param name="handle">폴더 주인의 사용자명</param>
    /// <param name="slug">폴더의 slug</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBookmarkFavorite>> RemoveBookmarkFavoriteAsync(string handle, string slug, CancellationToken cancellationToken = default)
        => SendJsonAsync<SolvedBookmarkFavorite>(HttpMethod.Delete, $"account/bookmark-favorites/{Segment(handle)}/{Segment(slug)}", null, cancellationToken);
    #endregion

    sealed class FoldersEnvelope<T>
    {
        public List<T> Folders { get; init; } = [];
    }

    sealed record ProblemIdsBody(List<int> ProblemIds);

    sealed record SlugsBody(List<string> Slugs);
}
