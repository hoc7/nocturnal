using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BoneGame.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData", order = 0)]
    public class DialogueData : ScriptableObject
    {
        [SerializeReference,InlineEditor]
        public List<DialogueEntity> Dialogues = new List<DialogueEntity>();
    }
}