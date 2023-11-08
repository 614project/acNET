namespace acNET.Problem
{
    /// <summary>
    /// 수준별 문제
    /// </summary>
    public class Level : Type.BaseBody
    {
        /// <summary>
        /// 레벨값 마다 지정된 레벨 이름이 모인 문자열 배열
        /// </summary>
        public static readonly string[] Names = @"Unrated
Bronze V
Bronze IV
Bronze III
Bronze II
Bronze I
Silver V
Silver IV
Silver III
Silver II
Silver I
Gold V
Gold IV
Gold III
Gold II
Gold I
Platinum V
Platinum IV
Platinum III
Platinum II
Platinum I
Diamond V
Diamond IV
Diamond III
Diamond II
Diamond I
Ruby V
Ruby IV
Ruby III
Ruby II
Ruby I
Master".Replace('\r','\0').Split('\n');

        /// <summary>
        /// 레벨(티어)값입니다.
        /// </summary>
        public long level;
        /// <summary>
        /// 이 문제 수준인 문제 수입니다.
        /// </summary>
        public long count;
        /// <summary>
        /// 문제의 레벨(티어)값을 레벨 이름으로 가져옵니다.
        /// </summary>
        public string GetTierName => Names[this.level];
    }
}
