using System;
using BoneGame.Event;
using BoneGame.Message;
using BoneGame.System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace BoneGame.Dialogue
{
    public class DialogueManager : InputMonoBehaviour
    {
        [SerializeField]
        private DialogueData Data;

        private int NowIndex;
        private int MaxIndex;

        private void Start()
        {
            IsActive = true;
            Registration();
            
            MaxIndex = Data.Dialogues.Count;
            NowIndex = 0;
            Data.Dialogues[NowIndex].SendMessage();
            StartDialogue();
        }

        private void StartDialogue()
        {
            StartDialogueMessage message = new StartDialogueMessage();
            Messenger.Publish(message);
        }


        public override void FireAction(InputAction.CallbackContext context)
        {
            NowIndex++;
            if (Data != null && NowIndex >= 0 && NowIndex < MaxIndex)
            {
                // Data[NowIndex] exists.
                // Do something with Data[NowIndex]
                Data.Dialogues[NowIndex].SendMessage();
            }
            else
            {
                // Data[NowIndex] does not exist.
                // Handle this case
                EndDialogueMessage message = new EndDialogueMessage();
                Messenger.Publish(message);

                EventEndMessage eventEndMessage = new EventEndMessage();
                Messenger.Publish(eventEndMessage);
            }
        }

        public override void MoveAction(InputAction.CallbackContext context)
        {
        }
        
    }
}