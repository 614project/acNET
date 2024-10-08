﻿namespace acNET.User;

/// <summary>
/// 사용자가 속한 조직 목록를 가져옵니다.
/// </summary>
public class Organization : Type.Jsonable
{
    /// <summary>
    /// 조직의 ID입니다.
    /// </summary>
    public int organizationId { get; set; }
    /// <summary>
    /// 조직의 레이팅입니다.
    /// </summary>
    public int rating { get; set; }
    /// <summary>
    /// 조직에 포함된 사용자의 수입니다.
    /// </summary>
    public int userCount { get; set; }
    /// <summary>
    /// 조직의 총 난이도 기여 수입니다.
    /// </summary>
    public int voteCount { get; set; }
    /// <summary>
    /// 조직의 총 푼 문제 수입니다.
    /// </summary>
    public int solvedCount { get; set; }
    /// <summary>
    /// 조직의 이름입니다.
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 조직의 구분입니다.
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 조직의 색깔입니다.
    /// </summary>
    public string color { get; set; }

    /// <summary>
    /// 조직 타입을 Enum 형식으로 가져옵니다.
    /// </summary>
    public OrganizationType GetEnumType
    {
        get => this.type switch
        {
            "community" => OrganizationType.community,
            "university" => OrganizationType.university,
            "company" => OrganizationType.company,
            "high_school" => OrganizationType.high_school,
            _ => OrganizationType.unknown
        };
    }
}

/// <summary>
/// 조직 타입을 Enum으로 정리한것.
/// </summary>
public enum OrganizationType 
{
    community,
    university,
    company,
    high_school,
    /// <summary>
    /// 알수없음 (오류)
    /// </summary>
    unknown
}
