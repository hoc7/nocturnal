using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Data;
using BoneGame.Data.Celestial;
using BoneGame.Event;
using BoneGame.System;
using BoneGame.System.Sound;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium
{
    /// <summary>
    /// ゲームモード全体の入口
    /// </summary>
    public class PlanetariumStarter : MonoBehaviour
    {
        [SerializeField] private ObserverData DebugDaa;
        [SerializeField] private GameMaster _gameMaster;
        [SerializeField] private CelestialPresenter _celestial;
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
            _celestial.Initialization(DebugDaa.Latitude,270,time);
            
            time.StartTimer();

        }
    }
}