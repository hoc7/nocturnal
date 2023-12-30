using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BoneGame.Message;
using Cysharp.Threading.Tasks;

namespace BoneGame.Event
{

        [Serializable]
        public abstract class EventActionBase
        {
            public abstract UniTask StartAction(EventQueueData queueData, Queue<EventActionBase> eventActionBases,CancellationTokenSource source);

            public virtual async UniTask CallNextAction(EventQueueData queueData, Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
            {
                if (!eventActionBases.Any())
                {
                   Messenger.Publish(new EventClearMessage(queueData.EventId));
                    return;
                }

                var next = eventActionBases.Dequeue();
                await next.StartAction(queueData, eventActionBases,source);
            }
        }
    }
