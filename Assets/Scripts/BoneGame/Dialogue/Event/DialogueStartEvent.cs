using System;
using System.Collections.Generic;
using System.Threading;
using BoneGame.Event;
using BoneGame.Message;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace BoneGame.Dialogue.Event
{
   
        /// <summary>
        /// 会話イベントの発生
        /// </summary>
        [Serializable]
        public class DialogueStartEvent : EventActionBase
        {
            [SerializeField] private DialogueData Dialogue;
            public override async UniTask StartAction(int eventId, Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
            {
                AwakeDialogueMessage message = new AwakeDialogueMessage(Dialogue);
                Messenger.Publish(message);
                var x = await Messenger.Receive<EndDialogueMessage>().Take(1).ToUniTask();
            
                await CallNextAction(eventId, eventActionBases, source);
            }
        
    }
}