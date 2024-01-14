using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class StarData
    {
        public string StarName;
        public int HitId;
        [SerializeField]
        private Vector3 _position;

        public Vector3 GetPosition
        {
            get
            {
                return new Vector3(_position.x * scale, _position.y * scale, _position.z * scale);
            }
        }
        public Color color;
        public int scale = 1000;

        /// <summary>
        /// 等級
        /// </summary>
        public float Magnitude;

        public StarData(int hitId, Vector3 position, Color color, float magnitude)
        {
            HitId = hitId;
            _position = position;
            this.color = color;
            Magnitude = magnitude;
        }

        public StarData()
        {
            
        }
    }
}