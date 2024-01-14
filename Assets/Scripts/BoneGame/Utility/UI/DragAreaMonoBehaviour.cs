using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace BoneGame.System.UI
{
    /// <summary>
    /// ドラッグ領域のPresenter
    /// </summary>
    public class DragAreaMonoBehaviour : MonoBehaviour
    {
        [SerializeField] protected RectTransform RectTransform;
        public DragAreaPosition DragAreaPosition = new DragAreaPosition();
        protected DraggablePositionModel PositionModel;
        [SerializeField] protected DragArea DragArea = new DragArea();

        [Button("初期化")]
        public void Init()
        {
            DragAreaPosition.SetRectTransform(this.RectTransform);
            DragArea.SetPosition(DragAreaPosition);
        }

        public virtual void Initialization(DraggablePositionModel draggablePositionModel)
        {
            PositionModel = draggablePositionModel;
            PositionModel.Subject.Subscribe(_ =>
            {
                if (_ == null) return;

                if (DragAreaPosition.IsWithinRectangle(_.GetRectTransform().anchoredPosition))
                {
                    PositionModel.RemoveSubject.OnNext(_);
                    DragArea.SetNewPosition(_);
                }
                DragArea.AllPositionSet();
            }).AddTo(this);

            PositionModel.RemoveSubject.Subscribe(_ =>
            {
                if (_ == null) return;
                if (DragArea.IsContain(_))
                {
                    DragArea.Remove(_);
                }
                DragArea.AllPositionSet();
            }).AddTo(this);
        }
    }
}