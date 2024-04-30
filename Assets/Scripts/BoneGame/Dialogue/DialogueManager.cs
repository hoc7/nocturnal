using System;
using BoneGame.Event;
using BoneGame.Message;
using BoneGame.System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace BoneGame.Dialogue
{
    public class DialogueManager : InputMonoBehaviour
    {
        private DialogueData Data;

        private int NowIndex;
        private int MaxIndex;

        private void Start()
        {
            Registration();
        }

        private void StartDialogue(DialogueData data)
        {
            Data = data;
            IsActive = true;
           
            
            MaxIndex = Data.Dialogues.Count;
            NowIndex = 0;
            Data.Dialogues[NowIndex].SendMessage();
            
            StartDialogueMessage message = new StartDialogueMessage();
            Messenger.Publish(message);
        }

        private void Awake()
        {
            Messenger.Receive<AwakeDialogueMessage>().Subscribe(_ =>
            {
                StartDialogue(_.DialogueData);

            }).AddTo(this);
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

                // EventEndMessage eventEndMessage = new EventEndMessage();
                // Messenger.Publish(eventEndMessage);

                Data = null;
            }
        }

        public override void MoveAction(InputAction.CallbackContext context)
        {
        }
        
    }
}