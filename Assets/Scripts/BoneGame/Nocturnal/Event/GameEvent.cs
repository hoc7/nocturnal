using System;
using System.Collections.Generic;
using System.Threading;
using BoneGame.Adv;
using BoneGame.Data;
using BoneGame.Nocturnal.Planetarium;
using BoneGame.System;
using BoneGame.System.Sound;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoneGame.Event
{
    [Serializable]
    public class GameEvent : EventActionBase
    {
        [SerializeField] private int GameId;

        public override async UniTask StartAction(int eventId,
            Queue<EventActionBase> eventActionBases, CancellationTokenSource source)
        {
            await StartGame(GameId);
            await CallNextAction(eventId, eventActionBases, source);
        }

        private async UniTask StartGame(int gameId)
        {
            BoneSoundManager.Instance.StopBGM();
            JObject jObject = new JObject();
            jObject.Add("gameId", gameId);
            SceneStartEventBase eventBase = new SceneStartEventBase(jObject);
            await SceneLoader.Instance.MoveScene("Game", eventBase);

            await Observable.Timer(TimeSpan.FromMilliseconds(100));

            var objects = SceneManager.GetActiveScene().GetRootGameObjects();
            PlanetariumStarter planetariumStarter = null;
            foreach (var go in objects)
            {
                if (go.TryGetComponent<PlanetariumStarter>(out PlanetariumStarter bus))
                {
                    planetariumStarter = bus;
                    break;
                }
            }

            GameMaster master = MasterDataHolder.Instance.GetGame(gameId);
            await planetariumStarter.PlayGame(master);
        }
    }
}