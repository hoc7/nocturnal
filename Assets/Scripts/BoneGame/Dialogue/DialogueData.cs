using System.Collections.Generic;
using BoneGame.System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BoneGame.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData", order = 0)]
    public class DialogueData : MasterDataScriptableObject
    {
        [LabelText("会話パート内容")]
        [OdinSerialize]
        [InlineProperty]
        public List<DialogueEntity> Dialogues = new List<DialogueEntity>();
    }
}