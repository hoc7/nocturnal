using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoneGame.System;
using Sirenix.OdinInspector;
using UnityEngine;
using BoneGame.Event;
using BoneGame.Nocturnal.Data;
using BoneGame.Nocturnal.Planetarium;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace BoneGame.Data
{
    public class MasterDataHolder : SingletonMonoBehaviour<MasterDataHolder>
    {
        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        [SerializeField] private List<GameMaster> GameMasters;
        [SerializeField] private List<EventMaster> EventMasters;
        [SerializeField] private StarMasters starMasters;
        [SerializeField] private SignMasters signMasters;

        /// <summary>
        /// イベントの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EventMaster GetEvent(int id)
        {
            var getEvent = EventMasters.FirstOrDefault(_ => _.Id == id);
            return getEvent;
        }

        /// <summary>
        /// ゲームの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameMaster GetGame(int id)
        {
            var getEvent = GameMasters.FirstOrDefault(_ => _.Id == id);
            return getEvent;
        }
        
        /// <summary>
        /// 星データの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StarData GetStar(int id)
        {
            var star = starMasters.GetStar(id);
            return star;
        }

        public List<StarData> GetAllStar()
        {
            return starMasters.GetStars;
        }

        public List<SignData> GetAllSign()
        {
            return signMasters.GetAllSignData();
        }

        /// <summary>
        /// 星座データの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SignData GetSine(int id)
        {
            var sign = signMasters.GetSignData(id);
            return sign;
        }
        

#if UNITY_EDITOR

        [Button("マスターデータ全設定")]
        public void SetMasters()
        {
            GameMasters = FindMaster<GameMaster>();
            EventMasters = FindMaster<EventMaster>();
            starMasters = FindMaster<StarMasters>().FirstOrDefault();
            signMasters = FindMaster<SignMasters>().FirstOrDefault();
        }

        /// <summary>
        /// マスターの検索
        /// </summary>
        /// <returns></returns>
        public static List<T> FindMaster<T>()
        {
            var typeName = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append("t:");
            builder.Append(typeName);
            var guids = UnityEditor.AssetDatabase.FindAssets(builder.ToString());

            List<T> masters = new List<T>();
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var objects = AssetDatabase.LoadAllAssetsAtPath(path).ToList();
                masters.AddRange(objects.OfType<T>().ToList());
            }

            return masters;
        }

        [Button("EventId重複チェック")]
        void Checkeventid()
        {
            var events = EventMasters;
            List<int> ids = events.Select(_ => _.Id).ToList();

            HashSet<int> uniqueNumbers = new HashSet<int>();
            HashSet<int> duplicateNumbers = new HashSet<int>();

            foreach (int number in ids)
            {
                if (!uniqueNumbers.Add(number))
                {
                    duplicateNumbers.Add(number);
                }
            }

            foreach (int duplicate in duplicateNumbers)
            {
                Debug.Log("重複した数字: " + duplicate);
            }
        }
#endif
    }
}