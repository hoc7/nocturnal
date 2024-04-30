using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BoneGame.Data.Adv
{
    public class StageSceneManager : SerializedMonoBehaviour
    {
        
        public Dictionary<string, GameObject> BgStage;

        public void SetStage(string key)
        {
            foreach ( GameObject bgStageValue in BgStage.Values)
            {
                bgStageValue.SetActive(false);
            }

            BgStage[key].gameObject.SetActive(true);
        }
    }

}