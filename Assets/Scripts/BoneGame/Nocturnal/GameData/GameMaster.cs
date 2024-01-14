using System;
using System.Collections.Generic;
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
        /// ゲームで探す星のId
        /// </summary>
        public List<int> SearchStarId;
        
        /// <summary>
        /// ゲームで探す星座のId
        /// </summary>
        public List<int> SearchSignId;
    }
}