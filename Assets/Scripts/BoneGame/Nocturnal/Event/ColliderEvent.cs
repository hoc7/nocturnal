using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Event;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BoneGame.Nocturnal.Event
{
    [LabelText("接触発火イベント")]
    public class ColliderEvent:MonoBehaviour
    {
        public List<EventMaster> Events = new List<EventMaster>();

        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartEvent().Forget();
            }
        }
        
        //////////////////////
        ////  Trigger系　////
        ////////////////////
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //コライダーが当たったら最初に呼ばれる
            //collisionに相手側の情報が格納される。
            Debug.Log(collision.name);
            StartEvent().Forget();
        }

        private async UniTask StartEvent()
        {
            var ev = Events.FirstOrDefault(_ => _.CheckTrigger());
            if (ev == null) return;
            await EventStarter.Instance.StartEvent(ev.Id,ev.Actions);
        }
    }
}