using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Event;
using UnityEngine;

namespace BoneGame.Nocturnal.Event
{
    /// <summary>
    /// そのシーン開始時に呼ばれるイベント
    /// </summary>
    public class StartEvent : MonoBehaviour
    {
        public List<EventMaster> Events = new List<EventMaster>();

        private async void Start()
        {
            var ev = Events.FirstOrDefault(_ => _.CheckTrigger());
            if (ev == null) return;
            await EventStarter.Instance.StartEvent(ev.Id,ev.Actions);
        }
    }
}