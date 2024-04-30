using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Event.Trigger;
using BoneGame.System;
using Codice.Client.Common.GameUI.Checkin;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BoneGame.Event
{
    [CreateAssetMenu(fileName = "EventMaster", menuName = "EventMaster作成", order = 100)]
    public class EventMaster : MasterDataScriptableObject
    {
        [LabelText("条件トリガー")]
        [OdinSerialize]
        [InlineProperty]
        public List<EventTriggerBase> Triggers = new List<EventTriggerBase>();

        [LabelText("これが立ってたら発火しないトリガー")]
        [OdinSerialize]
        [InlineProperty]
        public List<EventTriggerBase> CannotExecTriggers = new List<EventTriggerBase>();



        public bool CheckTrigger()
        {
            if (CannotExecTriggers.Any())
            {
                if (CannotExecTriggers.All(_ => _.CheckTrigger()))
                {
                    return false;
                }
            }

            if (Triggers.Any())
            {
                return Triggers.All(_ => _.CheckTrigger());
            }

            return true;
        }


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