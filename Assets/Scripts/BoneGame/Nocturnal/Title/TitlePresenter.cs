
using BoneGame.Data;
using BoneGame.Event;
using BoneGame.Message;
using BoneGame.System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BoneGame.Nocturnal.Title
{
    public class TitlePresenter : MonoBehaviour,ISceneInitializer
    {
        [SerializeField] private Button StartButton;
        [SerializeField] private int StartEventId;


        private void Start()
        {
            StartButton.OnClickAsObservable().Subscribe(async _ =>
            {
                await StartGame();
            }).AddTo(this);
        }


        private async UniTask StartGame()
        {
            EventMaster master = MasterDataHolder.Instance.GetEvent(this.StartEventId);
            await EventStarter.Instance.StartEvent(master.Id,master.Actions);
        }

        public void Initialization(SceneStartEventBase eventBase)
        {
            
        }
    }
}