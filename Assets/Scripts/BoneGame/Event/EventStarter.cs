using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BoneGame.System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoneGame.Event
{
    /// <summary>
    /// イベントを開始し、管理する暮らす
    /// イベントが終わったら破棄
    /// </summary>
    public class EventStarter
    {
        private static EventStarter _instance;

        public static EventStarter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventStarter();
                }
                return _instance;
            }
        }
        
        private CancellationTokenSource CancellationToken;
        private bool NowPlaying;
        private EventData NowData;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            _instance = null;
        }
        
        
        /// <summary>
        /// Eventの開始
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="actionBases"></param>
        public async UniTask StartEvent(int eventId,List<EventActionBase> actionBases)
        {
            try
            {
                if (NowPlaying)
                {
                    return;
                }

                NowData = new EventData(eventId,actionBases);

                if (CancellationToken != null)
                {
                    CancellationToken.Dispose();
                }

                CancellationToken = new CancellationTokenSource();
                await StartAreaEvent(CancellationToken);
            }
            catch(Exception e)
            {
                NowPlaying = false;
                CancellationToken = null;
                Debug.LogError($"Error: {e.Message}");
            }
        }
        
        private async UniTask StartAreaEvent(CancellationTokenSource cancellationToken)
        {
            if (NowData.ActionQueue.Count == 0) return;
            var action = NowData.ActionQueue.Dequeue();
            await action.StartAction(NowData.EventId,NowData.ActionQueue, cancellationToken);
            NowPlaying = false;
            NowData = null;
        }

        private class EventData
        {
            public int EventId { get; private set; }
            public Queue<EventActionBase> ActionQueue { get; private set; }
            public EventData(int eventId,List<EventActionBase> actionBases)
            {
                EventId = eventId;
                ActionQueue = new Queue<EventActionBase>(actionBases);
            }

        }
    }
}