using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BoneGame.Event.Adv;
using BoneGame.Message;
using BoneGame.System;
using Com.LuisPedroFonseca.ProCamera2D;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Utage;

namespace BoneGame.Event
{
    // /// <summary>
    // /// start adv event
    // /// </summary>
    // [Serializable]
    // public class AdvPrefabEvent : EventActionBase
    // {
    //     [SerializeField] private string Label;
    //
    //     public override async UniTask StartAction(int eventId,
    //         Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
    //     {
    //         AdvStartModel model = new AdvStartModel();
    //         await model.StartAdv_prefab(this.Label);
    //         await CallNextAction(eventId, eventActionBases, source);
    //     }
    // }
    //
    // /// <summary>
    // /// start adv event
    // /// </summary>
    // [Serializable]
    // public class AdvSceneEvent : EventActionBase
    // {
    //     [SerializeField] private string Label;
    //
    //     public override async UniTask StartAction(int eventId,
    //         Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
    //     {
    //         AdvStartModel model = new AdvStartModel();
    //      
    //         await model.StartAdv_scene(this.Label);
    //         await CallNextAction(eventId, eventActionBases, source);
    //     }
    // }



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
        private Vector3 Rotation;
        
        public override async UniTask StartAction(int eventId,
            Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            var gameObject = GameObject.Find(ObjectName);
            gameObject.transform.position = new Vector3(Position.x,Position.y,Position.z);
            gameObject.transform.rotation = Quaternion.Euler(Rotation.x,Rotation.y,Rotation.z);
            await CallNextAction(eventId, eventActionBases, source);
        }
    }

    public class ProCameraTargetTransformChange: EventActionBase
    {
        [SerializeField] private string ObjectName;
     
        
        public override async UniTask StartAction(int eventId,
            Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            var gameObject = GameObject.Find(ObjectName);
            var target = ProCamera2D.Instance.CameraTargets.FirstOrDefault();
            target.TargetTransform = gameObject.transform;
            await CallNextAction(eventId, eventActionBases, source);
        }
    }
}