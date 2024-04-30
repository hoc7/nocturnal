using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Data;
using BoneGame.Data.Celestial;
using BoneGame.Event;
using BoneGame.Message;
using BoneGame.Nocturnal.GameData;
using BoneGame.Nocturnal.Planetarium.Book;
using BoneGame.Nocturnal.Planetarium.Purpose;
using BoneGame.System;
using BoneGame.System.Sound;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UniRx;

namespace BoneGame.Nocturnal.Planetarium
{
    /// <summary>
    /// ゲームモード全体の入口
    /// </summary>
    public class PlanetariumStarter : MonoBehaviour
    {
        [SerializeField] private TimeDrawer TimeDrawer;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private ObserverData DebugDaa;
        [SerializeField] private GameMaster _gameMaster;
        [SerializeField] private CelestialPresenter _celestial;
        [SerializeField] private PurposePresenter _purposePresenter;
        [SerializeField] private BookPresenter _bookPresenter;
        private GameTime _time;

        [LabelText("プレイ時間(秒)")]
        public int GameSecond;

        [LabelText("何時間経過するか")] public int GameHour;
        [SerializeField] private int Year;
        [SerializeField] private int Month;
        [SerializeField] private int Day;
        [SerializeField] private int Hour;

        public bool Test = false;

        private void Start()
        {
            if (Test)
            {
                PlayGame(_gameMaster);
            }
        }

        public void PlayGame(GameMaster master)
        {
            Test = false;
            BoneSoundManager.Instance.PlayBGM(master.GetAudioClip);
            GameTime time = new GameTime(GameSecond,GameHour * 3600,Year,Month,Day,Hour,0);

            // いったん日本の9月で決め打ち
            _celestial.Initialization(90 - DebugDaa.Latitude, 15 + (Month * 30), time);
            _cameraController.Initialization(90 - DebugDaa.Latitude);
            _purposePresenter.Initialization(master.GetPurpose());
            _bookPresenter.Initialization(MasterDataHolder.Instance.GetAllBook());
            time.StartTimer();

            time.TimeElapsed.Subscribe(_ =>
            {
                TimeDrawer.DrawTime(time._currentTime);
            }).AddTo(this);
            
            time.TimerIsEnd.Subscribe(_ =>
            {
                // 終了処理
                GameEndMessage message = new GameEndMessage();
                Messenger.Publish(message);
            }).AddTo(this);
        }
    }
}