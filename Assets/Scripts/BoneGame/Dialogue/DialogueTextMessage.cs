namespace BoneGame.Dialogue
{
    /// <summary>
    /// Dialogueのテキストを選択する
    /// </summary>
    public class DialogueTextMessage
    {
        public Talk Entity;

        public DialogueTextMessage(Talk entity)
        {
            Entity = entity;
        }
    }

    public class EndDialogueMessage
    {
        
    }

    public class StartDialogueMessage
    {
        
    }
    
}