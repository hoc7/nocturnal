using System;

namespace BoneGame.Dialogue
{
    [Serializable]
    public abstract class DialogueEntity
    {
        public abstract void SendMessage();
    }
}