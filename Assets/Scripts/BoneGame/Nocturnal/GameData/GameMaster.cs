using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Nocturnal.GameData;
using UnityEngine;

namespace BoneGame.Event
{
    [CreateAssetMenu(fileName = "GameMaster", menuName = "GameMaster作成", order = 100)]
    public class GameMaster : MasterDataScriptableObject
    {
        [SerializeField] private AudioClip AudioClip;
        public AudioClip GetAudioClip => AudioClip;
        public float Time;
        /// <summary>
        /// ゲームで探す星のMaster
        /// </summary>
        public List<PurposeStarMaster> SearchStarId;
        
        /// <summary>
        /// ゲームで探す星座のMaster
        /// </summary>
        public List<PurposeSignMaster> SearchSignId;

        public List<PurposeMasterBase> GetPurpose()
        {
            List<PurposeMasterBase> masterBases = new List<PurposeMasterBase>();
            masterBases.AddRange(SearchSignId);
            masterBases.AddRange(SearchStarId);
            return masterBases;
        }
    }
}