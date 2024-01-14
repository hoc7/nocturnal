using BoneGame.Event;
using BoneGame.Message;
using BoneGame.Nocturnal.Planetarium;
using BoneGame.System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace BoneGame.Data
{
    public class GameSceneInitializer : MonoBehaviour
    {
        // [SerializeField] private PlanetariumPresenter PlanetariumPresenter;
        // private int NowGameId;
        // private int EndEventId;
        //
        // private void Start()
        // {
        //     Messenger.Receive<GameEndMessage>().Subscribe(async _ =>
        //     {
        //         await EndGame();
        //     }).AddTo(this);
        // }
        //
        // public void Initialization(SceneStartEventBase eventBase)
        // {
        //     NowGameId = (int)eventBase.JObject["gameId"];
        //     EndEventId = (int)eventBase.JObject["eventId"];
        //
        //     GameMaster gameMaster = MasterDataHolder.Instance.GetGame(NowGameId);
        //     
        //     PlanetariumPresenter.PlayGame(gameMaster);
        // }
        //
        //
        //
        // public async UniTask EndGame()
        // {
        //     var master = MasterDataHolder.Instance.GetEvent(EndEventId);
        //     //await EventStarter.Instance.StartEvent(master.Id,master.Actions);
        // }
    }
}