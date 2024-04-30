using System;
using System.Collections.Generic;
using BoneGame.Event;
using BoneGame.Nocturnal.GameData;
using UnityEngine;

namespace BoneGame.Data
{
    public class DebugStarter : MonoBehaviour
    {
        [SerializeField] private List<EventFlag> Flags = new List<EventFlag>(); 
        private void Awake()
        {
            foreach (var flag in Flags)
            {
                GameData.Instance().SetFrag(flag.Id);
            }
        }
    }
}