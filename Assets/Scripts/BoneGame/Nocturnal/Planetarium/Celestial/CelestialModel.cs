using System.Collections.Generic;
using BoneGame.Nocturnal.Planetarium;

namespace BoneGame.Data.Celestial
{
    /// <summary>
    /// 天球の星の情報を保持するModel
    /// </summary>
    public class CelestialModel
    {


        /// <summary>
        /// 1秒間に回転する角度
        /// </summary>
        public const float DegreesPerSecond = 0.5f;

        public float _startDegrees;

        /// <summary>
        /// 星の情報
        /// </summary>
        private List<StarData> _starDatas = new List<StarData>();

        /// <summary>
        /// 緯度
        /// </summary>
        private float _latitude;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="startDegrees"></param>
        /// <param name="starDatas"></param>
        public CelestialModel(float latitude,float startDegrees,List<StarData> starDatas)
        {
            _latitude = latitude;
            _startDegrees = startDegrees;
            _starDatas = starDatas;
        }

        /// <summary>
        /// 表示できる星のデータを表示
        /// 後々、緯度を確認することで、表示できる星とできない星を分別して返すメソッドにしたい。
        /// 表示しなくて良い星は作らない形にしたいためである。
        /// </summary>
        /// <returns></returns>
        public List<StarData> GetDisplayStarData()
        {
            return _starDatas;
        }

        /// <summary>
        /// 現在の天球の回転角度を取得
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public float GetAngle(float time)
        {
            return DegreesPerSecond / 100f;
        }
    }
}