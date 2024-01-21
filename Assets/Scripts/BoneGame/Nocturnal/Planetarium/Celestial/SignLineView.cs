using System.Collections.Generic;
using BoneGame.Data;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium.Celestial
{
    /// <summary>
    /// 星座線を書かせるView
    /// </summary>
    public class SignLineView : MonoBehaviour
    {
    
        [SerializeField] private GameObject Prefab;
        [SerializeField] private Transform CelestialTransform;
        /// <summary>
        /// 全ての星座線の描画
        /// </summary>
        /// <param name="signs"></param>
        public void DrawAllSignLine(List<SignData> signs)
        {
            foreach (SignData sign in signs)
            {
                foreach (HipLine line in sign._lines)
                {
                    Draw(line);
                }
            }
        }
        
        /// <summary>
        /// 星座線1ポンの描画
        /// </summary>
        /// <param name="hipLine"></param>
        private void Draw(HipLine hipLine)
        {
            GameObject instance = Instantiate(Prefab, CelestialTransform);
            SignLineDrawer drawer = instance.GetComponent<SignLineDrawer>();

            StarData startStar = MasterDataHolder.Instance.GetStar(hipLine.sttHipId);
            StarData endStar = MasterDataHolder.Instance.GetStar(hipLine.endHipId);
            
            drawer.DebugSetId(startStar.HitId,endStar.HitId);
            drawer.DrawLine(startStar.GetPosition,endStar.GetPosition);
        }
    }
}