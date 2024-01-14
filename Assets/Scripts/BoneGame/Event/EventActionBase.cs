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
            public abstract UniTask StartAction(int eventId, Queue<EventActionBase> eventActionBases,CancellationTokenSource source);

            public virtual async UniTask CallNextAction(int eventId, Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
            {
                if (!eventActionBases.Any())
                {
                   Messenger.Publish(new EventClearMessage(eventId));
                    return;
                }

                var next = eventActionBases.Dequeue();
                await next.StartAction(eventId, eventActionBases,source);
            }
        }
    }
