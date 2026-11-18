using AcNET.Account;
using AcNET.Achievement;

namespace AcNET;

public partial class SolvedAPI
{
    #region Account
    /// <summary>
    /// 요청한 사용자의 사이트 설정을 기본값이 적용된 상태로 가져옵니다. (<c>GET /account/settings</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>settings</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAccountSettings>> GetAccountSettingsAsync(CancellationToken cancellationToken = default)
        => GetAsync<SettingsEnvelope, SolvedAccountSettings>("account/settings", null, static e => e.Settings, cancellationToken);

    /// <summary>
    /// 요청한 사용자의 사이트 설정 중 일부를 바꿉니다. (<c>PATCH /account/settings</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>settings.write</c>)</remarks>
    /// <param name="settings">바꿀 설정. null 인 항목은 바뀌지 않습니다.</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>바뀐 뒤의 전체 설정</returns>
    public Task<SolvedResult<SolvedAccountSettings>> UpdateAccountSettingsAsync(SolvedAccountSettingsUpdate settings, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return SendJsonAsync<SettingsEnvelope, SolvedAccountSettings>(HttpMethod.Patch, "account/settings", settings, static e => e.Settings, cancellationToken);
    }

    /// <summary>
    /// 요청한 사용자의 수정 가능한 프로필 항목을 가져옵니다. (<c>GET /account/profile</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>profile.personal</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAccountProfile>> GetAccountProfileAsync(CancellationToken cancellationToken = default)
        => GetAsync<ProfileEnvelope, SolvedAccountProfile>("account/profile", null, static e => e.Profile, cancellationToken);

    /// <summary>
    /// 요청한 사용자가 장착할 수 있는 배경을 가져옵니다. (<c>GET /account/backgrounds</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>collection</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedOwnedBackground>>> GetAccountBackgroundsAsync(CancellationToken cancellationToken = default)
        => GetAsync<BackgroundsEnvelope, List<SolvedOwnedBackground>>("account/backgrounds", null, static e => e.Backgrounds, cancellationToken);

    /// <summary>
    /// 요청한 사용자가 아직 동의하지 않은 시행 중인 정책 버전을 가져옵니다. (<c>GET /account/consents</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>profile</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>모두 동의했다면 빈 목록</returns>
    public Task<SolvedResult<List<SolvedPendingConsent>>> GetAccountPendingConsentsAsync(CancellationToken cancellationToken = default)
        => GetAsync<ConsentsEnvelope, List<SolvedPendingConsent>>("account/consents", null, static e => e.Pending, cancellationToken);

    /// <summary>
    /// 요청한 사용자의 경고와 정지 상태를 가져옵니다. (<c>GET /account/standing</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>standing</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAccountStanding>> GetAccountStandingAsync(CancellationToken cancellationToken = default)
        => GetAsync<SolvedAccountStanding>("account/standing", null, cancellationToken);

    /// <summary>
    /// 요청한 사용자의 정지 중 이의 제기할 수 있거나 이의 제기한 정지를 가져옵니다. (<c>GET /account/bans</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>standing</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedAppealableBan>>> GetAccountBansAsync(CancellationToken cancellationToken = default)
        => GetAsync<ItemsEnvelope<SolvedAppealableBan>, List<SolvedAppealableBan>>("account/bans", null, static e => e.Items, cancellationToken);

    /// <summary>
    /// 요청한 사용자가 가진 뱃지 또는 배경 목록을 가져옵니다. (<c>GET /account/collection</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>collection</c>)</remarks>
    /// <param name="kind">아이템 종류</param>
    /// <param name="sort">정렬 기준</param>
    /// <param name="category">분류 필터</param>
    /// <param name="query">이름 검색어</param>
    /// <param name="offset">시작 위치</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedCollection>> GetAccountCollectionAsync(
        SolvedItemKind kind = SolvedItemKind.Badge,
        SolvedCollectionSort sort = SolvedCollectionSort.Recent,
        string? category = null,
        string? query = null,
        int offset = 0,
        CancellationToken cancellationToken = default)
        => GetAsync<SolvedCollection>("account/collection", CollectionQuery(kind, sort, category, query, offset), cancellationToken);

    /// <summary>
    /// 요청한 사용자가 가진 아이템과 스트릭 테마, 휴가 상태를 가져옵니다. (<c>GET /account/items</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>collection</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAccountItems>> GetAccountItemsAsync(CancellationToken cancellationToken = default)
        => GetAsync<SolvedAccountItems>("account/items", Lang(), cancellationToken);

    /// <summary>
    /// 요청한 사용자의 별가루 잔액과 오늘 받은 별가루를 가져옵니다. (<c>GET /account/wallet</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>wallet</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedWallet>> GetAccountWalletAsync(CancellationToken cancellationToken = default)
        => GetAsync<SolvedWallet>("account/wallet", null, cancellationToken);

    /// <summary>
    /// 요청한 사용자의 한 달 별가루 내역을 가져옵니다. (<c>GET /account/wallet/history</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>wallet</c>)</remarks>
    /// <param name="month">조회할 달 (연도와 월만 사용). null 이면 이번 달</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedWalletHistory>> GetAccountWalletHistoryAsync(DateOnly? month = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedWalletHistory>("account/wallet/history", Lang().Add("month", Month(month)), cancellationToken);

    /// <summary>
    /// 이모티콘 목록과 요청한 사용자의 보유 여부를 가져옵니다. (<c>GET /me/emoticons</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>collection</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedEmoticon>>> GetMyEmoticonsAsync(CancellationToken cancellationToken = default)
        => GetAsync<ItemsEnvelope<SolvedEmoticon>, List<SolvedEmoticon>>("me/emoticons", null, static e => e.Items, cancellationToken);
    #endregion

    sealed class SettingsEnvelope
    {
        public SolvedAccountSettings Settings { get; init; } = new();
    }

    sealed class ProfileEnvelope
    {
        public SolvedAccountProfile Profile { get; init; } = new();
    }

    sealed class BackgroundsEnvelope
    {
        public List<SolvedOwnedBackground> Backgrounds { get; init; } = [];
    }

    sealed class ConsentsEnvelope
    {
        public List<SolvedPendingConsent> Pending { get; init; } = [];
    }
}
