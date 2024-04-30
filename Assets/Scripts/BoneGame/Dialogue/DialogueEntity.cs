using System;
using Sirenix.OdinInspector;

namespace BoneGame.Dialogue
{
    [Serializable]
    public abstract class DialogueEntity
    {
        public abstract void SendMessage();
    }
}