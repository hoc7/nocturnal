using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoneGame.Event
{
    [CreateAssetMenu(fileName = "EventMaster", menuName = "EventMaster作成", order = 100)]
    public class EventMaster : MasterDataScriptableObject
    {
        /// <summary>
        /// 実行するイベント群
        /// </summary>
        public List<EventActionBase> Actions = new List<EventActionBase>();
    }
}