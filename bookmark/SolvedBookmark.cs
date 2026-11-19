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
    public string Slug { get; set; } = string.Empty;
    /// <summary>폴더 이름입니다.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>폴더 설명입니다.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>폴더 색입니다.</summary>
    public SolvedFolderColor? Color { get; set; }
    /// <summary>공개 범위입니다.</summary>
    public SolvedFolderVisibility Visibility { get; set; }
    /// <summary>폴더 안의 문제 수입니다.</summary>
    public int ProblemCount { get; set; }
    /// <summary>즐겨찾기한 사용자 수입니다.</summary>
    public int FavoriteCount { get; set; }
    /// <summary>폴더 안에서 요청한 사용자가 푼 문제 수입니다. 사용자 정보가 없으면 null 입니다.</summary>
    public int? SolvedCount { get; set; }
    /// <summary>폴더 주인입니다.</summary>
    public SolvedUserRef Owner { get; set; } = new();
    /// <summary>만든 시각입니다.</summary>
    public string CreatedAt { get; set; } = string.Empty;
    /// <summary>수정한 시각입니다.</summary>
    public string UpdatedAt { get; set; } = string.Empty;
}

/// <summary>
/// 요청한 사용자의 북마크 폴더입니다. (<see cref="SolvedAPI.GetAccountBookmarkFoldersAsync"/>)
/// </summary>
public class SolvedAccountBookmarkFolder : SolvedBookmarkFolder
{
    /// <summary>지정한 문제가 이 폴더에 들어있는지 여부입니다.</summary>
    public bool ContainsProblem { get; set; }
}

/// <summary>
/// 북마크 폴더 목록 한 페이지입니다.
/// </summary>
public class SolvedBookmarkFolderPage : Jsonable
{
    /// <summary>폴더 목록입니다.</summary>
    public List<SolvedBookmarkFolder> Folders { get; set; } = [];
    /// <summary>전체 폴더 수입니다.</summary>
    public int Total { get; set; }
    /// <summary>현재 페이지입니다.</summary>
    public int Page { get; set; }
    /// <summary>페이지 크기입니다.</summary>
    public int PageSize { get; set; }
}

/// <summary>
/// 북마크 폴더와 그 문제 한 페이지입니다. (<see cref="SolvedAPI.GetUserBookmarkFolderAsync"/>)
/// </summary>
public class SolvedBookmarkFolderDetail : Jsonable
{
    /// <summary>폴더입니다.</summary>
    public SolvedBookmarkFolder Folder { get; set; } = new();
    /// <summary>요청한 사용자가 폴더 주인인지 여부입니다.</summary>
    public bool IsOwner { get; set; }
    /// <summary>요청한 사용자가 즐겨찾기했는지 여부입니다. 사용자 정보가 없으면 null 입니다.</summary>
    public bool? IsFavorite { get; set; }
    /// <summary>레벨별 문제 수입니다.</summary>
    public List<SolvedLevel> Levels { get; set; } = [];
    /// <summary>이 페이지의 문제들입니다.</summary>
    public List<SolvedBookmarkedProblem> Problems { get; set; } = [];
    /// <summary>조건에 맞는 전체 문제 수입니다.</summary>
    public int Total { get; set; }
    /// <summary>현재 페이지입니다.</summary>
    public int Page { get; set; }
    /// <summary>페이지 크기입니다.</summary>
    public int PageSize { get; set; }
}

/// <summary>
/// 북마크 폴더 안의 문제입니다.
/// </summary>
public class SolvedBookmarkedProblem : Jsonable
{
    /// <summary>문제입니다.</summary>
    public SolvedProblem Problem { get; set; } = new();
    /// <summary>메모입니다.</summary>
    public string? Note { get; set; }
    /// <summary>폴더에 넣은 시각입니다.</summary>
    public string AddedAt { get; set; } = string.Empty;
}

/// <summary>
/// 폴더를 바꾼 결과입니다.
/// </summary>
public class SolvedBookmarkFolderChange : Jsonable
{
    /// <summary>바뀐 뒤의 폴더입니다.</summary>
    public SolvedBookmarkFolder Folder { get; set; } = new();
    /// <summary>실제로 바뀐 문제 수입니다.</summary>
    public int Changed { get; set; }
}

/// <summary>
/// 폴더 즐겨찾기 상태입니다.
/// </summary>
public class SolvedBookmarkFavorite : Jsonable
{
    /// <summary>즐겨찾기 중인지 여부입니다.</summary>
    public bool IsFavorite { get; set; }
    /// <summary>즐겨찾기한 사용자 수입니다.</summary>
    public int FavoriteCount { get; set; }
}

/// <summary>
/// 새로 만들 북마크 폴더입니다. (<see cref="SolvedAPI.CreateBookmarkFolderAsync"/>)
/// </summary>
public class SolvedBookmarkFolderCreate : Jsonable
{
    /// <summary>
    /// 만들 폴더를 정합니다.
    /// </summary>
    /// <param name="slug">폴더의 slug (영문 소문자, 숫자, '_' 1~32자)</param>
    /// <param name="name">폴더 이름</param>
    public SolvedBookmarkFolderCreate(string slug, string name)
    {
        Slug = Guard.NotNullOrEmpty(slug);
        Name = Guard.NotNullOrEmpty(name);
    }

    /// <summary>폴더의 slug 입니다. (영문 소문자, 숫자, '_' 1~32자)</summary>
    public string Slug { get; }
    /// <summary>폴더 이름입니다.</summary>
    public string Name { get; }
    /// <summary>폴더 설명입니다.</summary>
    public string? Description { get; set; }
    /// <summary>폴더 색입니다.</summary>
    public SolvedFolderColor? Color { get; set; }
    /// <summary>공개 범위입니다.</summary>
    public SolvedFolderVisibility? Visibility { get; set; }
}

/// <summary>
/// 바꿀 북마크 폴더 정보입니다. (<see cref="SolvedAPI.UpdateBookmarkFolderAsync"/>) null 인 항목은 바뀌지 않습니다.
/// </summary>
public class SolvedBookmarkFolderUpdate : Jsonable
{
    /// <summary>새 slug 입니다.</summary>
    public string? Slug { get; set; }
    /// <summary>새 이름입니다.</summary>
    public string? Name { get; set; }
    /// <summary>새 설명입니다.</summary>
    public string? Description { get; set; }
    /// <summary>새 색입니다.</summary>
    public SolvedFolderColor? Color { get; set; }
    /// <summary>새 공개 범위입니다.</summary>
    public SolvedFolderVisibility? Visibility { get; set; }
}

/// <summary>
/// 폴더 안 문제의 위치나 메모를 바꾸는 요청입니다. (<see cref="SolvedAPI.UpdateBookmarkProblemAsync"/>) null 인 항목은 보내지 않습니다.
/// </summary>
public class SolvedBookmarkProblemUpdate : Jsonable
{
    /// <summary>새 메모입니다. 빈 문자열을 보내면 메모를 지웁니다.</summary>
    public string? Note { get; set; }
    /// <summary>이 문제 번호 앞으로 옮깁니다.</summary>
    public int? Before { get; set; }
    /// <summary>이 문제 번호 뒤로 옮깁니다.</summary>
    public int? After { get; set; }
}
