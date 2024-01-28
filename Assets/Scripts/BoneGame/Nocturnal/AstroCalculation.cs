using UnityEngine;

namespace BoneGame.Data
{
    /// <summary>
    /// 天体に関する計算を行うクラス
    /// </summary>
    public static class AstroCalculation
    {
        /// <summary>
        /// 地軸の傾き
        /// </summary>
        public const float Declination = 23.5f;
        
        /// <summary>
        /// 赤緯と赤経からゲーム内での座標を表示する
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetStarPosition(Quaternion dec, Quaternion ra)
        {
            return  ra * dec *Vector3.forward;
        }

        /// <summary>
        /// 赤緯からQuaternionを取得
        /// </summary>
        /// <param name="decH">赤緯（度）</param>
        /// <param name="decM">赤緯（分）</param>
        /// <param name="decS">赤緯（秒）</param>
        /// <param name="sign">符号</param>
        /// <returns></returns>
        public static Quaternion GetDecToCelestialQuaternion(int decH, int decM, float decS, int sign)
        {
            float dec = (decH + decM / 60f + decS / 3600f) * (sign == 0 ? -1f : 1f);
            Quaternion rotDec = Quaternion.AngleAxis(dec, Vector3.right);
            return rotDec;
        }

        /// <summary>
        /// 赤経からQuaternionを取得
        /// </summary>
        /// <param name="raH">赤経（時）</param>
        /// <param name="raM">赤経（分）</param>
        /// <param name="raS">赤経（秒）</param>
        /// <returns></returns>
        public static Quaternion GetRaToCelestialQuaternion(int raH, int raM, float raS)
        {
            float ra = (360f / 24f) * (raH + raM / 60f + raS / 3600f);
            return Quaternion.AngleAxis(ra, Vector3.up);
        }

        /// <summary>
        /// 北極点の取得
        /// </summary>
        /// <returns></returns>
        public static Quaternion GetNorthPosition()
        {
            // 北極は赤緯90°
            return GetDecToCelestialQuaternion(90, 0, 0, 1);
        }

        /// <summary>
        /// 南極点の取得
        /// </summary>
        /// <returns></returns>
        public static Quaternion GetSouthPosition()
        {
            // 南極は赤緯-90°
            return GetDecToCelestialQuaternion(90, 0, 0, 0);
        }

        /// <summary>
        /// 観測者の平面の傾きを緯度と地軸から計算
        /// </summary>
        /// <param name="latitude">緯度</param>
        /// <returns></returns>
        public static float GetInclinationFromLatitude(float latitude)
        {
            return Declination - latitude;
        }
        
        public static Vector3 GetEarthAxis()
        {
            return new Vector3(0, 23.5f, 0f);
        }
    }
}