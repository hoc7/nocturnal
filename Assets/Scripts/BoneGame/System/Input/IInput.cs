using BoneGame.Message;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoneGame.System
{
    public abstract class InputMonoBehaviour : MonoBehaviour
    {
        public bool IsActive;
        public abstract void MoveAction(InputAction.CallbackContext context);
        public abstract void FireAction(InputAction.CallbackContext context);

        public void Registration()
        {
            RegistFireEventMessage message = new RegistFireEventMessage(FireAction);
            Messenger.Publish(message);

            RegistMoveEventMessage moveEventMessage = new RegistMoveEventMessage(MoveAction);
            Messenger.Publish(moveEventMessage);
        }
    }
}