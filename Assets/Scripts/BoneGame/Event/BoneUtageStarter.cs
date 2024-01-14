using System.Collections;
using System.Collections.Generic;
using BoneGame.System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Utage;

namespace BoneGame.Adv
{


    public class BoneUtageStarter : MonoBehaviour, ISceneInitializer
    {
        [SerializeField] private string label;
        [SerializeField] private AdvEngineStarter AdvEngineStarter;

        public void Initialization(SceneStartEventBase eventBase)
        {
            string labelName = (string)eventBase.JObject["labelName"];
            AdvEngineStarter.ScenariosName = labelName;
        }

        public async UniTask PlayScenario()
        {
            await AdvEngineStarter.StartEngine();
        }

        [Sirenix.OdinInspector.Button]
        private void Debug()
        {
            AdvEngineStarter.ScenariosName = label;
            AdvEngineStarter.StartEngine();
        }
    }
}