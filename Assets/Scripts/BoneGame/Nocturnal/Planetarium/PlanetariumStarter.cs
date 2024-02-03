using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Data;
using BoneGame.Data.Celestial;
using BoneGame.Event;
using BoneGame.Nocturnal.Planetarium.Purpose;
using BoneGame.System;
using BoneGame.System.Sound;
using Cysharp.Threading.Tasks;
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
        private GameTime _time;


        private void Start()
        {
            PlayGame(_gameMaster);
        }

        public async UniTask PlayGame(GameMaster master)
        {
            BoneSoundManager.Instance.PlayBGM(master.GetAudioClip);
            GameTime time = new GameTime();

            // いったん日本の9月で決め打ち
            _celestial.Initialization(90 - DebugDaa.Latitude, 15 + (DebugDaa.Month * 30), time);
            _cameraController.Initialization(90 - DebugDaa.Latitude);
            _purposePresenter.Initialization(master.GetPurpose());
            time.StartTimer();

            time.TimeElapsed.Subscribe(_ => { TimeDrawer.DrawTime(time._currentTime); }).AddTo(this);
        }
    }
}