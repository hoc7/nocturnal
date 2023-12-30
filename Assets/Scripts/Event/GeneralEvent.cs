using System;
using System.Collections.Generic;
using System.Threading;
using BoneGame.Event.Adv;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Utage;

namespace BoneGame.Event
{
    /// <summary>
    /// start adv event
    /// </summary>
    [Serializable]
    public class AdvEvent : EventActionBase
    {
        [SerializeField] private string Label;

        public override async UniTask StartAction(EventQueueData queueData,
            Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            AdvStartModel model = new AdvStartModel();
            await model.StartAdv(this.Label);
            await CallNextAction(queueData, eventActionBases, source);
        }
    }
}