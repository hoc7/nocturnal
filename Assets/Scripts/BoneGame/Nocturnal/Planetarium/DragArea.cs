using Sirenix.Serialization;
using TNRD;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoneGame.Nocturnal.Planetarium
{
    public class DragArea : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
    {
        
        [SerializeField]
        private  SerializableInterface<IDragComponent> DragComponent;
        public float rotationSpeed = 5.0f;
        private bool isDragging = false; // ドラッグ状態を追跡する変数
        
        
        // ポインター（マウス/タッチ）がオブジェクト上で押されたときに呼び出される
        public void OnPointerDown(PointerEventData eventData)
        {
            isDragging = true;
        }

        // ポインターがオブジェクトから離されたときに呼び出される
        public void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;
        }
        
        void Update()
        {
            if (isDragging && Input.GetMouseButton(0))
            {
                // マウスの移動量を取得
                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

                DragComponent.Value.Drag(mouseX,mouseY);
            }
        }
    }
}