using System;
using System.Collections.Generic;
using System.Threading;
using BoneGame.Data;
using BoneGame.Data.Adv;
using BoneGame.Event;
using BoneGame.Event.Adv;
using BoneGame.Message;
using BoneGame.Nocturnal.Planetarium;
using BoneGame.System;
using BoneGame.System.Sound;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameEndMessage = BoneGame.Nocturnal.Planetarium.GameEndMessage;

namespace BoneGame.Nocturnal.Event
{

        /// <summary>
        /// start adv event
        /// </summary>
        [Serializable]
        public class AdvSceneEvent : EventActionBase
        {
            [SerializeField] private string bgName;
            
            public override async UniTask StartAction(int eventId,
                Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
            {
                BoneSoundManager.Instance.StopBGM();
                JObject jObject = new JObject();
                jObject.Add("bgName", bgName);
                SceneStartEventBase eventBase = new SceneStartEventBase(jObject);
                await SceneLoader.Instance.MoveScene("AdvScene", eventBase);

                await Observable.Timer(TimeSpan.FromMilliseconds(100));

                var objects = SceneManager.GetActiveScene().GetRootGameObjects();
                StageSceneManager stagesmSceneManager = null;
                foreach (var go in objects)
                {
                    if (go.TryGetComponent<StageSceneManager>(out StageSceneManager ssm))
                    {
                        stagesmSceneManager = ssm;
                        break;
                    }
                }
                
                stagesmSceneManager.SetStage(this.bgName);
                
                await CallNextAction(eventId, eventActionBases, source);
            }
        }

    
}