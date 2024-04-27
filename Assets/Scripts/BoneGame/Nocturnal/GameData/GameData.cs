using System.Collections.Generic;

namespace BoneGame.Nocturnal.GameData
{
    /// <summary>
    /// 夜行性の夜のゲームデータ
    /// </summary>
    public class GameData
    {
        private static GameData _instance = new GameData();

        private GameData()
        {
        }

        public static GameData Instance()
        {
            return _instance;
        }

        public List<int> NowClearFlags { get; private set; } = new List<int>();

        /// <summary>
        /// フラグの設定
        /// </summary>
        /// <param name="flag"></param>
        public void SetFrag(int flag)
        {
            NowClearFlags.Add(flag);
        }
    }
}