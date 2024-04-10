using System;
using BoneGame.Message;
using Sirenix.OdinInspector;

namespace BoneGame.Dialogue
{
    [Serializable]
    public class Talk : DialogueEntity
    {
        public Actor Actor;
        [MultiLineProperty(4)]
        public string Text;
        
        public override void SendMessage()
        {
            DialogueTextMessage message = new DialogueTextMessage(this);
            Messenger.Publish(message);
        }
    }
}