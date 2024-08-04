using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoneGame.Message;
using BoneGame.System.Sound;
using NPOI.SS.Formula.Functions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BoneGame.Dialogue
{
    [Serializable]
    public class BGMChange: DialogueEntity
    {
        public string BGMFile;

        public override void SendMessage()
        {
            BoneSoundManager.Instance.PlayBGM(BGMFile);
        }
    }
}