using System.Collections.Generic;
using BoneGame.Nocturnal.Planetarium;
using UnityEngine;

namespace BoneGame.Data.Celestial
{
    /// <summary>
    /// 天球の表示に関連するView
    /// </summary>
    public class CelestialView : MonoBehaviour
    {
        [SerializeField] private GameObject Prefab;
        [SerializeField] private Transform CelestialTransform;
        private Vector3 axis;
        /// <summary>
        /// 地軸の傾きを初期設定する
        /// </summary>
        /// <param name="degrees"></param>
        public void InitAxis(float degrees)
        {
            axis = new Vector3(0, degrees, 0);
            CelestialTransform.rotation = Quaternion.Euler(axis);
        }
   
        /// <summary>
        /// 回転を行う
        /// </summary>
        /// <param name="degrees"></param>
        public void SetAngle(float degrees)
        {
            CelestialTransform.Rotate(axis, degrees);
        }

        /// <summary>
        /// 天球の星を作成する
        /// </summary>
        /// <param name="stars"></param>
        public void CreateStars(List<StarData> stars)
        {
            foreach (StarData star in stars)
            {
                GameObject instance = Instantiate(Prefab, CelestialTransform);
                StarView starView = instance.GetComponent<StarView>();
                starView.Set(star);
            }
        }
    }
}