using System;
using BoneGame.Message;
using BoneGame.System;
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


        private void Start()
        {
            StartButton.OnClickAsObservable().Subscribe(_ =>
            {
                StartGame();
                
            }).AddTo(this);
        }


        private void StartGame()
        {
            JObject jObject = new JObject();
            jObject.Add("labelName","adv_1");

            SceneStartEventBase eventBase = new SceneStartEventBase(jObject);
            SceneLoader.Instance.MoveScene("Adv",eventBase);
        }

        public void Initialization(SceneStartEventBase eventBase)
        {
            
        }
    }
}