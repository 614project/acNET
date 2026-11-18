using AcNET.Problem;
using AcNET.Type;
using AcNET.User;

namespace AcNET.Bookmark;

/// <summary>
/// 북마크 폴더입니다.
/// </summary>
public class SolvedBookmarkFolder : Jsonable
{
    /// <summary>폴더의 slug 입니다. (영문 소문자, 숫자, '_' 1~32자)</summary>
    public string Slug { get; init; } = string.Empty;
    /// <summary>폴더 이름입니다.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>폴더 설명입니다.</summary>
    public string Description { get; init; } = string.Empty;
    /// <summary>폴더 색입니다.</summary>
    public SolvedFolderColor? Color { get; init; }
    /// <summary>공개 범위입니다.</summary>
    public SolvedFolderVisibility Visibility { get; init; }
    /// <summary>폴더 안의 문제 수입니다.</summary>
    public int ProblemCount { get; init; }
    /// <summary>즐겨찾기한 사용자 수입니다.</summary>
    public int FavoriteCount { get; init; }
    /// <summary>폴더 안에서 요청한 사용자가 푼 문제 수입니다. 사용자 정보가 없으면 null 입니다.</summary>
    public int? SolvedCount { get; init; }
    /// <summary>폴더 주인입니다.</summary>
    public SolvedUserRef Owner { get; init; } = new();
    /// <summary>만든 시각입니다.</summary>
    public string CreatedAt { get; init; } = string.Empty;
    /// <summary>수정한 시각입니다.</summary>
    public string UpdatedAt { get; init; } = string.Empty;
}

/// <summary>
/// 요청한 사용자의 북마크 폴더입니다. (<see cref="SolvedAPI.GetAccountBookmarkFoldersAsync"/>)
/// </summary>
public class SolvedAccountBookmarkFolder : SolvedBookmarkFolder
{
    /// <summary>지정한 문제가 이 폴더에 들어있는지 여부입니다.</summary>
    public bool ContainsProblem { get; init; }
}

/// <summary>
/// 북마크 폴더 목록 한 페이지입니다.
/// </summary>
public class SolvedBookmarkFolderPage : Jsonable
{
    /// <summary>폴더 목록입니다.</summary>
    public List<SolvedBookmarkFolder> Folders { get; init; } = [];
    /// <summary>전체 폴더 수입니다.</summary>
    public int Total { get; init; }
    /// <summary>현재 페이지입니다.</summary>
    public int Page { get; init; }
    /// <summary>페이지 크기입니다.</summary>
    public int PageSize { get; init; }
}

/// <summary>
/// 북마크 폴더와 그 문제 한 페이지입니다. (<see cref="SolvedAPI.GetUserBookmarkFolderAsync"/>)
/// </summary>
public class SolvedBookmarkFolderDetail : Jsonable
{
    /// <summary>폴더입니다.</summary>
    public SolvedBookmarkFolder Folder { get; init; } = new();
    /// <summary>요청한 사용자가 폴더 주인인지 여부입니다.</summary>
    public bool IsOwner { get; init; }
    /// <summary>요청한 사용자가 즐겨찾기했는지 여부입니다. 사용자 정보가 없으면 null 입니다.</summary>
    public bool? IsFavorite { get; init; }
    /// <summary>레벨별 문제 수입니다.</summary>
    public List<SolvedLevel> Levels { get; init; } = [];
    /// <summary>이 페이지의 문제들입니다.</summary>
    public List<SolvedBookmarkedProblem> Problems { get; init; } = [];
    /// <summary>조건에 맞는 전체 문제 수입니다.</summary>
    public int Total { get; init; }
    /// <summary>현재 페이지입니다.</summary>
    public int Page { get; init; }
    /// <summary>페이지 크기입니다.</summary>
    public int PageSize { get; init; }
}

/// <summary>
/// 북마크 폴더 안의 문제입니다.
/// </summary>
public class SolvedBookmarkedProblem : Jsonable
{
    /// <summary>문제입니다.</summary>
    public SolvedProblem Problem { get; init; } = new();
    /// <summary>메모입니다.</summary>
    public string? Note { get; init; }
    /// <summary>폴더에 넣은 시각입니다.</summary>
    public string AddedAt { get; init; } = string.Empty;
}

/// <summary>
/// 폴더를 바꾼 결과입니다.
/// </summary>
public class SolvedBookmarkFolderChange : Jsonable
{
    /// <summary>바뀐 뒤의 폴더입니다.</summary>
    public SolvedBookmarkFolder Folder { get; init; } = new();
    /// <summary>실제로 바뀐 문제 수입니다.</summary>
    public int Changed { get; init; }
}

/// <summary>
/// 폴더 즐겨찾기 상태입니다.
/// </summary>
public class SolvedBookmarkFavorite : Jsonable
{
    /// <summary>즐겨찾기 중인지 여부입니다.</summary>
    public bool IsFavorite { get; init; }
    /// <summary>즐겨찾기한 사용자 수입니다.</summary>
    public int FavoriteCount { get; init; }
}

/// <summary>
/// 새로 만들 북마크 폴더입니다. (<see cref="SolvedAPI.CreateBookmarkFolderAsync"/>)
/// </summary>
public class SolvedBookmarkFolderCreate : Jsonable
{
    /// <summary>폴더의 slug 입니다. (영문 소문자, 숫자, '_' 1~32자)</summary>
    public required string Slug { get; init; }
    /// <summary>폴더 이름입니다.</summary>
    public required string Name { get; init; }
    /// <summary>폴더 설명입니다.</summary>
    public string? Description { get; init; }
    /// <summary>폴더 색입니다.</summary>
    public SolvedFolderColor? Color { get; init; }
    /// <summary>공개 범위입니다.</summary>
    public SolvedFolderVisibility? Visibility { get; init; }
}

/// <summary>
/// 바꿀 북마크 폴더 정보입니다. (<see cref="SolvedAPI.UpdateBookmarkFolderAsync"/>) null 인 항목은 바뀌지 않습니다.
/// </summary>
public class SolvedBookmarkFolderUpdate : Jsonable
{
    /// <summary>새 slug 입니다.</summary>
    public string? Slug { get; init; }
    /// <summary>새 이름입니다.</summary>
    public string? Name { get; init; }
    /// <summary>새 설명입니다.</summary>
    public string? Description { get; init; }
    /// <summary>새 색입니다.</summary>
    public SolvedFolderColor? Color { get; init; }
    /// <summary>새 공개 범위입니다.</summary>
    public SolvedFolderVisibility? Visibility { get; init; }
}

/// <summary>
/// 폴더 안 문제의 위치나 메모를 바꾸는 요청입니다. (<see cref="SolvedAPI.UpdateBookmarkProblemAsync"/>) null 인 항목은 보내지 않습니다.
/// </summary>
public class SolvedBookmarkProblemUpdate : Jsonable
{
    /// <summary>새 메모입니다. 빈 문자열을 보내면 메모를 지웁니다.</summary>
    public string? Note { get; init; }
    /// <summary>이 문제 번호 앞으로 옮깁니다.</summary>
    public int? Before { get; init; }
    /// <summary>이 문제 번호 뒤로 옮깁니다.</summary>
    public int? After { get; init; }
}
