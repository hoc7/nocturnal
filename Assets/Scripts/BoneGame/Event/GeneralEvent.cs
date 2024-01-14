using System;
using System.Collections.Generic;
using System.Threading;
using BoneGame.Event.Adv;
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
}