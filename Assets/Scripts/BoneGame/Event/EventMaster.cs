using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BoneGame.Event
{
    [CreateAssetMenu(fileName = "EventMaster", menuName = "EventMaster作成", order = 100)]
    public class EventMaster : MasterDataScriptableObject
    {
        /// <summary>
        /// 実行するイベント群
        /// </summary>
        [SerializeField,InlineEditor]
        public List<EventActionBase> _actions { get; private set; } = new List<EventActionBase>();

        /// <summary>
        /// Getter
        /// </summary>
        /// <returns></returns>
        public List<EventActionBase> Actions => _actions;
    }
}