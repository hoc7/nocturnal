using System;
using BoneGame.System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoneGame.System.UI
{
    public class RightClickHandler:SingletonMonoBehaviour<RightClickHandler>
    {
        private Action NowRightClickAction;

        private void Update()
        {
            if (Input.GetMouseButtonUp(1))
            {
                NowRightClickAction?.Invoke();
            }
        }

        private void Start()
        {
            NowRightClickAction = DebugLog;
        }
        private void DebugLog()
        {
            Debug.Log("右クリックが押されました");
        }

        public void SetEvent(Action action)
        {
            NowRightClickAction = action;
        }

        public void RemoveEvent()
        {
            NowRightClickAction = null;
        }

        public Action GetNowEvent()
        {
            return NowRightClickAction;
        }
    }
}