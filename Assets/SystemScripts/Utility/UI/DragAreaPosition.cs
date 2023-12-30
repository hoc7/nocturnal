using System;
using UnityEngine;

namespace BoneGame.System.UI
{
    /// <summary>
    /// ドラッグしたものがエリア座標内にあるかどうかを判定するクラス
    /// </summary>
    [Serializable]
    public class DragAreaPosition
    {
        public RectTransform AreaRect;
        public Vector2 LeftDown;
        public Vector2 RightUp;
        public Vector2 LeftUp => new Vector2(LeftDown.x,RightUp.y);

        public bool IsWithinRectangle(Vector2 pos)
        {
            return pos.x >= LeftDown.x && pos.x <= RightUp.x && pos.y >= LeftDown.y && pos.y <= RightUp.y;
        }

        public void SetRectTransform(RectTransform rectTransform)
        {
            AreaRect = rectTransform;
            LeftDown = GetLeftDown(rectTransform);
            RightUp = GetRightUp(rectTransform);
        }
        
        Vector2 GetLeftDown(RectTransform rectTransform)
        {
            var position = rectTransform.localPosition;
            var sizeDelta = rectTransform.sizeDelta;
            float x = position.x - (sizeDelta.x / 2);
            float y = position.y - (sizeDelta.y / 2);
            return new Vector2(x,y);
        }

        Vector2 GetRightUp(RectTransform rectTransform)
        {
            var position = rectTransform.localPosition;;
            var sizeDelta = rectTransform.sizeDelta;
            float x = position.x + (sizeDelta.x / 2);
            float y = position.y + (sizeDelta.y / 2);
            return new Vector2(x,y);
        }
    }
}