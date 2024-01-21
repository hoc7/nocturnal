using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium
{
    /// <summary>
    /// 星座線事態のコンポーネント
    /// </summary>
    public class SignLineDrawer : MonoBehaviour
    {
        public int StartStarId;
        public int EndStarId;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float DefaultLineWidth; // ここで任意の線の太さを設定


        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>() ?? gameObject.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = DefaultLineWidth; // 線の開始部分の太さを設定
            _lineRenderer.endWidth = DefaultLineWidth; // 線の終了部分の太さを設定
            this.transform.eulerAngles = new Vector3(0, 0f, 0f);
        }

        public void DrawLine(Vector3 start, Vector3 end)
        {
            // LineRendererに2点間の座標を設定します
            _lineRenderer.positionCount = 2; // 2点間の線を描くためには、2つの座標が必要です
            _lineRenderer.SetPosition(0, start); // 最初の点（index=0）を設定します
            _lineRenderer.SetPosition(1, end); // 二番目の点（index=1）を設定します
        }

        public void DebugSetId(int startid, int endId)
        {
            StartStarId = startid;
            EndStarId = endId;
        }
    }
}