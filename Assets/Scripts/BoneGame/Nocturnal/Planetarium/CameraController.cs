using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace BoneGame.Nocturnal.Planetarium
{

    public class CameraController : MonoBehaviour,IDragComponent
    {
        [SerializeField] private Slider ZoomSlider;
        [SerializeField]
        Camera Camera;
        
        public float rotationSpeed = 5.0f;
        private float pitch = 0.0f; // X軸回転（上下の向き）
        private float yaw = 0.0f;   // Y軸回転（左右の向き）

        private void Start()
        {
            ZoomSlider.OnValueChangedAsObservable().Subscribe(_ =>
            {
                Camera.fieldOfView = 35 - (_ * 30);

            }).AddTo(this);
        }

        public void Drag(float x, float y )
        {
            // マウスの移動量を取得

            // カメラの回転量を計算
            yaw += x;
            pitch -= y;
          //  pitch = Mathf.Clamp(pitch, -90f, 0f); // 下を向かないように制限

            // カメラの回転を適用
            Camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        
    }
}