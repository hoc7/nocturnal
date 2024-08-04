using System;
using System.Linq;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

namespace BoneGame.Dialogue
{
    [Serializable]
    public class CameraMove : DialogueEntity
    {
        [SerializeField] private string ObjectName = "CameraPosObject";
        public Vector3 MovePosition;

        public override void SendMessage()
        {
            Move(ObjectName, MovePosition);
        }
        
        private void Move(string objectName, Vector3 postiion)
        {
            var gameObject = GameObject.Find(objectName);
            gameObject.transform.position = new Vector3(postiion.x, postiion.y, postiion.z);
            var target = ProCamera2D.Instance.CameraTargets.FirstOrDefault();
            target.TargetTransform = gameObject.transform;
        }
    }
}