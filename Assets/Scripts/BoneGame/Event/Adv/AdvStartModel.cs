using System;
using BoneGame.Event.GeneralEvent;
using BoneGame.Message;
using BoneGame.System;
using BoneGame.System.Sound;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;
using Object = UnityEngine.Object;
using BoneGame.Adv;

namespace BoneGame.Event.Adv
{
    /// <summary>
    /// start scenario part
    /// </summary>
    public class AdvStartModel
    {
        private const string Address = "AdvPrefab";
        public async UniTask StartAdv_prefab(string label)
        {
            BoneSoundManager.Instance.StopBGM();
            GameObject advresource = await ResourceLoader.Load<GameObject>(Address);
            GameObject adv = Object.Instantiate(advresource);
            AdvEngineStarter starter = adv.GetComponentInChildren<AdvEngineStarter>();
            starter.Engine.StartScenarioLabel = label;
            await starter.StartEngine();
            Object.Destroy(adv);
            await UniTask.DelayFrame(5);
        }

        public async UniTask StartAdv_scene(string label)
        {
            BoneSoundManager.Instance.StopBGM();
            JObject jObject = new JObject();
            jObject.Add("labelName",label);
            SceneStartEventBase eventBase = new SceneStartEventBase(jObject);
            
            await SceneLoader.Instance.MoveScene("Adv",eventBase);

            await Observable.Timer(TimeSpan.FromMilliseconds(100));

            var objects = SceneManager.GetActiveScene().GetRootGameObjects();
            BoneUtageStarter boneUtageStarter = null;
            foreach (var go in objects)
            {
                if(go.TryGetComponent<BoneUtageStarter>(out BoneUtageStarter bus))
                {
                    boneUtageStarter = bus;
                    break;
                }
            }

            if (boneUtageStarter == null) return;
            await boneUtageStarter.PlayScenario();
            await UniTask.DelayFrame(5);
        }
    }
}