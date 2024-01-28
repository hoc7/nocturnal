using System;

namespace BoneGame.Data
{
    /// <summary>
    /// 観測者のデータ
    /// </summary>
    [Serializable]
    public class ObserverData
    {
        /// <summary>
        /// 観測者の緯度
        /// </summary>
        public int Latitude;
        
        /// <summary>
        /// 季節
        /// </summary>
        public int Month;
    }
}