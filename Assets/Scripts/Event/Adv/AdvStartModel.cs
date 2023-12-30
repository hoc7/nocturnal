using BoneGame.System;
using BoneGame.System.Sound;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utage;

namespace BoneGame.Event.Adv
{
    /// <summary>
    /// start scenario part
    /// </summary>
    public class AdvStartModel
    {
        private const string Address = "AdvPrefab";
        public async UniTask StartAdv(string label)
        {
            BoneSoundManager.Instance.StopBGM();
            GameObject advresource = await ResourceLoader.Load<GameObject>(Address);
            GameObject adv = Object.Instantiate(advresource);
            AdvEngineStarter starter = adv.GetComponentInChildren<AdvEngineStarter>();
            starter.Engine.StartScenarioLabel = label;
            await starter.StartEngine();
            Object.Destroy(adv);
            await UniTask.DelayFrame(1);
        }
    }
}