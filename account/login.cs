﻿using acNET.Type;

namespace acNET.Account
{
    /// <summary>
    /// 로그인을 통해 얻을수 있는 자세한 유저 정보
    /// </summary>
    public class DetailedUser : User.RankedUser
    {
        /// <summary>
        /// 계정의 이메일 주소입니다.
        /// </summary>
        public string email;
        /// <summary>
        /// 사용자의 solved.ac 설정 정보입니다.
        /// </summary>
        public Settings settings;
    }

    /// <summary>
    /// 사용자의 solved.ac 설정 정보입니다.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// 사이트 디자인 테마입니다.
        /// </summary>
        public string screenTheme;
        /// <summary>
        /// 태그 이름을 나타낼 때 사용할 언어입니다.
        /// </summary>
        public string tagDisplayLanguage;
        /// <summary>
        /// 해결한 문제의 난이도 아이콘입니다.
        /// </summary>
        public string iconSchemeSolved;
        /// <summary>
        /// 해결하지 못한 문제의 난이도 아이콘입니다.
        /// </summary>
        public string iconSchemeNotSolved;
        /// <summary>
        /// 문제 목록의 기본 정렬 순서입니다.
        /// </summary>
        public string problemSortBy;
        /// <summary>
        /// 트윗에 핸들을 포함하는지 여부입니다.
        /// </summary>
        public string twitterPostHandle;
        /// <summary>
        /// CLASS가 올랐을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        public string twitterPostOnClassIncrease;
        /// <summary>
        /// 문제를 처음 해결했을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        public string twitterPostOnProblemSolve;
        /// <summary>
        /// AC 레이팅이 올랐을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        public string twitterPostOnRatingIncrease;
        /// <summary>
        /// 티어가 올랐을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        public string twitterPostOnTierIncrease;

        /// <summary>
        /// 트윗에 핸들을 포함하는지 여부입니다. (bool)
        /// </summary>
        public bool GetTwitterPostHandleBoolean => this.twitterPostHandle == "true";
        /// <summary>
        /// CLASS가 올랐을 때 트윗을 보내는지 여부입니다. (bool)
        /// </summary>
        public bool GetTwitterPostOnClassIncreaseBoolean => this.twitterPostOnClassIncrease == "true";
        /// <summary>
        /// 문제를 처음 해결했을 때 트윗을 보내는지 여부입니다. (bool)
        /// </summary>
        public bool GetTwitterPostOnProblemSolveBoolean => this.twitterPostOnProblemSolve == "true";
        /// <summary>
        /// AC 레이팅이 올랐을 때 트윗을 보내는지 여부입니다. (bool)
        /// </summary>
        public bool GetTwitterPostOnRatingIncreaseBoolean => this.twitterPostOnRatingIncrease == "true";
        /// <summary>
        /// 티어가 올랐을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        public bool GetTwitterPostOnTierIncreaseBoolean => this.twitterPostOnTierIncrease == "true";
    }

    /// <summary>
    /// 사용자가 푼 문제에 대한 정보
    /// </summary>
    public class Solved
    {
        /// <summary>
        /// 문제 ID입니다.
        /// </summary>
        public long id;
        /// <summary>
        /// 현재 문제 풀이 상태입니다. 알려진 값은 다음이 있습니다.
        /// </summary>
        public string tried;
    }

    /// <summary>
    /// 현재 로그인한 계정 정보
    /// </summary>
    public class UserAccount : BaseBody
    {
        /// <summary>
        /// 계정의 사용자 정보입니다.
        /// </summary>
        public DetailedUser user;
        /// <summary>
        /// 해당 계정의 사용자가 푼 문제 정보입니다.
        /// </summary>
        public List<Solved> solved;
    }
}
