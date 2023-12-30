using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BoneGame.System.UI
{
    /// <summary>
    /// 指定領域内でアイコンの位置設定を行うクラス
    /// </summary>
    [Serializable]
    public class DragArea
    {
        private List<DraggableSelectable> NowSelectables = new List<DraggableSelectable>();
        public int NowCount => NowSelectables.Count;
        public float Padding_x;
        public float Padding_y;
        [LabelText("横に配置可能な数")] public int ColumnLimit;
        [SerializeField]
        public DragAreaPosition DragAreaPosition;

        public void SetPosition(DragAreaPosition position)
        {
            DragAreaPosition = position;
        }
        
        public void SetNewPosition(DraggableSelectable selectable)
        {
            NowSelectables.Add(selectable);
            AllPositionSet();
        }

        public void ResetSelectalbes()
        {
            NowSelectables = new List<DraggableSelectable>();
        }

        public void Remove(DraggableSelectable selectable)
        {
            if(!IsContain(selectable)) return;
            NowSelectables.Remove(selectable);
            AllPositionSet();
        }
        

        public void AllPositionSet()
        {
            foreach (var selectable in NowSelectables)
            {
                int index = NowSelectables.IndexOf(selectable);
                selectable.GetRectTransform().DOLocalMove(GetPosition(selectable,index), 0.4f);
            }
        }

        public bool IsContain(DraggableSelectable selectable)
        {
            return NowSelectables.Contains(selectable);
        }

        private Vector2 GetPosition(DraggableSelectable selectable,int index)
        {
            Vector2 pos = new Vector2(DragAreaPosition.LeftUp.x + GetColumn(index) * Padding_x,
                DragAreaPosition.LeftUp.y + GetLines(index) * Padding_y);

            //　左上の頂点を基準に並べているので、オブジェクトのサイズ分左上からずらす
            float x = selectable.GetRectTransform().sizeDelta.x / 2;
            float y = selectable.GetRectTransform().sizeDelta.y / 2;
            return new Vector2(pos.x + x, pos.y - y);
        }

        private int GetLines(int index)
        {
            int line = (index) / ColumnLimit;
            return line;
        }

        private int GetColumn(int index)
        {
            int column = (index) % ColumnLimit;
            return column;
        }
    }
}