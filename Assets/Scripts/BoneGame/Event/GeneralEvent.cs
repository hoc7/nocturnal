using System;
using System.Collections.Generic;
using System.Threading;
using BoneGame.Event.Adv;
using BoneGame.Message;
using BoneGame.System;
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
    public class AdvPrefabEvent : EventActionBase
    {
        [SerializeField] private string Label;

        public override async UniTask StartAction(int eventId,
            Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            AdvStartModel model = new AdvStartModel();
            await model.StartAdv_prefab(this.Label);
            await CallNextAction(eventId, eventActionBases, source);
        }
    }
    
    /// <summary>
    /// start adv event
    /// </summary>
    [Serializable]
    public class AdvSceneEvent : EventActionBase
    {
        [SerializeField] private string Label;

        public override async UniTask StartAction(int eventId,
            Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            AdvStartModel model = new AdvStartModel();
         
            await model.StartAdv_scene(this.Label);
            await CallNextAction(eventId, eventActionBases, source);
        }
    }

    /// <summary>
    /// 会話イベントの発生
    /// </summary>
    [Serializable]
    public class DialogueStartEvent : EventActionBase
    {
        [SerializeField] private string DialogueAddress;
        public override async UniTask StartAction(int eventId, Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            GameObject dialogue =
            await ResourceLoader.Load<GameObject>(DialogueAddress, false);
            
            var x = await Messenger.Receive<EventEndMessage>().Take(1).ToUniTask();
            
            await CallNextAction(eventId, eventActionBases, source);
        }
    }

    /// <summary>
    /// move scene
    /// </summary>
    public class ChangeSceneEvent : EventActionBase
    {
        [SerializeField] private string SceneName;
        
        public override async UniTask StartAction(int eventId,
            Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            await SceneLoader.Instance.MoveScene(SceneName);
            await CallNextAction(eventId, eventActionBases, source);
        }
    }

    /// <summary>
    /// Transformを設定する
    /// </summary>
    public class TransformChangeEvent : EventActionBase
    {
        [SerializeField] private string ObjectName;
        [SerializeField]
        private Vector3 Position;
        [SerializeField]
        private Vector4 Quaternion;
        
        public override async UniTask StartAction(int eventId,
            Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            var gameObject = GameObject.Find(ObjectName);
            gameObject.transform.position = new Vector3(Position.x,Position.y,Position.z);
            gameObject.transform.rotation = new Quaternion(Quaternion.x, Quaternion.y, Quaternion.z, Quaternion.w);
            await CallNextAction(eventId, eventActionBases, source);
        }
    }
}