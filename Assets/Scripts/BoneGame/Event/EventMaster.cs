using System;
using System.Collections.Generic;
using BoneGame.Event.Trigger;
using BoneGame.System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BoneGame.Event
{
    [CreateAssetMenu(fileName = "EventMaster", menuName = "EventMaster作成", order = 100)]
    public class EventMaster : MasterDataScriptableObject
    {
        [OdinSerialize]
        [InlineProperty]
        public List<EventTriggerBase> Triggers = new List<EventTriggerBase>();
        
        /// <summary>
        /// 実行するイベント群
        /// </summary>
        [SerializeField,InlineProperty]
        public List<EventActionBase> _actions { get; private set; } = new List<EventActionBase>();

        /// <summary>
        /// Getter
        /// </summary>
        /// <returns></returns>
        public List<EventActionBase> Actions => _actions;
    }
}