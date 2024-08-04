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
    
    
    /// <summary>
    /// アニメーションを変更する
    /// </summary>
    public class DialogueAnimationMessage
    {
        public ActorAnimation Entity;

        public DialogueAnimationMessage(ActorAnimation entity)
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

    public class AwakeDialogueMessage
    {
        public DialogueData DialogueData;

        public AwakeDialogueMessage(DialogueData dialogueData)
        {
            DialogueData = dialogueData;
        }
    }
    
}