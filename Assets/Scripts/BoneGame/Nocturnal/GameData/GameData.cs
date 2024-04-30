using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        
        [InitializeOnEnterPlayMode]
        static void OnEnterPlaymodeInEditor(EnterPlayModeOptions options)
        {
            Debug.Log("Entering PlayMode");

            if (options.HasFlag(EnterPlayModeOptions.DisableDomainReload))
                _instance = new GameData();
        }

        public static GameData Instance()
        {
            return _instance;
        }

        private List<int> NowClearFlags  = new List<int>();


        public List<int> GetFrag()
        {
            return NowClearFlags;
        }
        
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