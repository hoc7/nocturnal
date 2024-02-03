using System.Collections.Generic;

namespace BoneGame.Data
{
    /// <summary>
    /// 目的達成をチェックするメッセージ
    /// </summary>
    public class PurposeCheckMessage
    {
        public List<int> signs { get; private set; }
        public List<int> stars { get; private set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="signs"></param>
        /// <param name="stars"></param>
        public PurposeCheckMessage(List<int> signs, List<int> stars)
        {
            this.signs = signs;
            this.stars = stars;
        }
    }
}