using System;
using UniRx;

namespace BoneGame.System.UI
{
    /// <summary>
    /// ドラッグの状態を管理するクラス
    /// </summary>
    [Serializable]
    public class DraggablePositionModel
    {
        public DraggableSelectable NowSelectDraggableSelectable;
        public Subject<DraggableSelectable> Subject = new Subject<DraggableSelectable>();
        public Subject<DraggableSelectable> RemoveSubject = new Subject<DraggableSelectable>();

        public void StartDrag(DraggableSelectable selectable)
        {
            NowSelectDraggableSelectable = selectable;
        }

        public void EndDrag()
        {
            if(NowSelectDraggableSelectable == null) return;
            Subject.OnNext(NowSelectDraggableSelectable);
            NowSelectDraggableSelectable = null;
        }
    }
}