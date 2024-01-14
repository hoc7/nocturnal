using System.Net.NetworkInformation;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace BoneGame.Data.Celestial
{
    /// <summary>
    /// 天球の作成に関連するPresenter
    /// </summary>
    public class CelestialPresenter : MonoBehaviour
    {
        /// <summary>
        /// 天球に関連する情報を保持したモデル
        /// </summary>
        private CelestialModel _celestialModel;

        private GameTime _gameTime;

        /// <summary>
        /// 天球の見かけを描画するクラス
        /// </summary>
        [SerializeField] private CelestialView _view;

        /// <summary>
        /// 緯度を入力して初期化
        /// </summary>
        /// <param name="latitude">緯度</param>
        /// <param name="startDegrees">開始時点の天球の回転角度</param>
        /// <param name="gameTime">Game時間のクラス</param>
        public void Initialization(float latitude,float startDegrees,GameTime gameTime)
        {
            _celestialModel = new CelestialModel(latitude,startDegrees,MasterDataHolder.Instance.GetAllStar());
            _view.InitAxis(CelestialModel.Declination);
            
            gameTime.TimeElapsed.Subscribe(_ =>
            {
                _view.SetAngle(_celestialModel.GetAngle(_));
            }).AddTo(this);

            gameTime.TimerIsEnd.Subscribe(_ =>
            {
                // 終了処理
                End();
            }).AddTo(this);
            
            _view.CreateStars(_celestialModel.GetDisplayStarData());
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        private void End()
        {
            _gameTime.Dispose();
        }
        
    }
}