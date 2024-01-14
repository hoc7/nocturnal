using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoneGame.System.UI
{
    public class DragAreaController : MonoBehaviour
    {
        [SerializeField] private List<DragAreaMonoBehaviour> DragAreas;
        protected DraggablePositionModel model;
        protected void Awake()
        {
            model = new DraggablePositionModel();
            DragAreas.ForEach(_ => _.Initialization(model));
        }
    }
}