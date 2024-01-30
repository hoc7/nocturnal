using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace BoneGame.Nocturnal.Planetarium
{

    public class CameraController : MonoBehaviour,IDragComponent
    {
        [SerializeField] private GameObject _plane;// 地平
        [SerializeField] private Slider ZoomSlider;
        [SerializeField] UnityEngine.Camera Camera;
        [SerializeField] private Vector2 SliderValue;

        // 観測者座標の設定
        public float rotationSpeed = 5.0f;
        private float pitch = -15.0f; // X軸回転（上下の向き）
        private float yaw = 0.0f;   // Y軸回転（左右の向き）
        private float LatitudeAdjust;

        private void Start()
        {
            ZoomSlider.OnValueChangedAsObservable().Subscribe(_ =>
            {
                float a = SliderValue.y - SliderValue.x;
                Camera.fieldOfView = SliderValue.y + -1 * (_ * a);

            }).AddTo(this);
        }

        public void Initialization(float latitude)
        {
            LatitudeAdjust = latitude;
            Vector3 rotationVector = new Vector3(LatitudeAdjust, 0, 0);
            Vector3 startRotationVector = new Vector3(LatitudeAdjust + pitch, 0, 0);
            Quaternion rotation = Quaternion.Euler(rotationVector);
            Quaternion startRotation = Quaternion.Euler(startRotationVector);
            Camera.transform.rotation = startRotation;
            pitch = LatitudeAdjust;
            _plane.transform.rotation = rotation;
        }

        public void Drag(float x, float y )
        {
            // マウスの移動量を取得

            // 新しい軸（平面の垂直方向）を取得
            Vector3 newAxis = GetAxis();
            
            // カメラの回転量を計算
            yaw += x * rotationSpeed;
            pitch -= y　* rotationSpeed;
            pitch = Mathf.Clamp(pitch , -90f + LatitudeAdjust, 0f + LatitudeAdjust); // 下を向かないように制限

            // カメラの回転を適用
            Quaternion rotationY = Quaternion.AngleAxis(yaw, newAxis);
            // カメラを垂直に回転させるクォータニオンを作成
            Quaternion rotationX = Quaternion.Euler(pitch, 0, 0);

            // カメラの回転を適用（両方の回転を組み合わせる）
            Camera.transform.rotation = rotationY * rotationX;
        }

        public Vector3 GetAxis()
        {
            return _plane.transform.up;
        }


        
    }
}