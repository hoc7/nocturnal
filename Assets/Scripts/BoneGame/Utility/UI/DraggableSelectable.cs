using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BoneGame.System.UI
{
    /// <summary>
    /// ドラッグ可能なSelectable
    /// </summary>
    public class DraggableSelectable : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        [SerializeField] protected Selectable Selectable;
        protected  Vector3 screenPoint;
        protected  Vector3 offset;
        protected  DraggablePositionModel Model;

        public RectTransform GetRectTransform()
        {
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            return rectTransform;
        }

        public void SetModel(DraggablePositionModel model)
        {
            Model = model;
        }

        private void Start()
        {
            Selectable.OnPointerDownAsObservable().Subscribe(_ => { Model.StartDrag(this); }).AddTo(this);
            Selectable.OnPointerUpAsObservable().Subscribe(_ => { Model.EndDrag();}).AddTo(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // マウスのクリックを検出したら、OnDragイベントハンドラを追加する
            ExecuteEvents.Execute(gameObject, eventData, ExecuteEvents.beginDragHandler);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // マウスの位置を取得する
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            var position = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
            // Buttonコンポーネントを移動する
            transform.position = new Vector3(position.x, position.y, 0);
        }
    }
}