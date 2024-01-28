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
        [SerializeField] private LineRenderer _earthAxisView;
        private Vector3 axis;
        /// <summary>
        /// 地軸の傾きを初期設定する
        /// </summary>
        /// <param name="degrees"></param>
        public void InitAxis()
        {
            axis = AstroCalculation.GetEarthAxis();
            DrawAxis();
            //CelestialTransform.rotation = Quaternion.Euler(axis);
        }

        private void DrawAxis()
        {
            Vector3 northPole = new Vector3(0, 2000, 0);
            Vector3 southPole = new Vector3(0, -2000, 0);
            _earthAxisView.positionCount = 2;
            _earthAxisView.startWidth = 0.1f; // 線の開始部分の太さを設定
            _earthAxisView.endWidth = 0.1f; // 線の終了部分の太さを設定
            _earthAxisView.SetPosition(0, northPole);
            _earthAxisView.SetPosition(1, southPole);
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