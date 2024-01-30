using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Nocturnal.Planetarium;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BoneGame.Event
{
    [CreateAssetMenu(fileName = "StarMaster", menuName = "StarMaster作成", order = 100)]
    public class StarMasters : MasterDataScriptableObject
    {
        /// <summary>
        /// 星の情報一覧
        /// </summary>
        [SerializeField]
        public List<StarData> StarDataList = new List<StarData>();

        public float Scale;
        
        /// <summary>
        /// 星情報の取得
        /// </summary>
        /// <param name="hitId"></param>
        /// <returns></returns>
        public StarData GetStar(int hitId)
        {
            return StarDataList.FirstOrDefault(_ => _.HitId == hitId);
        }
        /// <summary>
        /// 全星データの取得
        /// </summary>
        public List<StarData> GetStars => StarDataList;
        
#if UNITY_EDITOR
        
        [SerializeField] private List<TextAsset> HipData;
        [Button("星情報の一括生成")]
        public void CreateMaster()
        {
            foreach (var csv in HipData)
            {
                var createdData = HipDataFactory.CreateHipDatas(csv);
                foreach (var starData in createdData)
                {
                    // 星Idが同じものがあればリストに入れない
                    if (StarDataList.Any(_ => _.HitId == starData.HitId)) continue;
                    StarDataList.Add(starData);
                }
               
            }
        }

#endif
    }
}