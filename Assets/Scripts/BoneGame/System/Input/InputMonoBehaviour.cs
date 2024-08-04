using BoneGame.Message;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoneGame.System
{
    public abstract class InputMonoBehaviour : MonoBehaviour
    {
        public bool IsActive;
        public abstract void MoveAction(InputAction.CallbackContext context,GameState.GameState state);
        public abstract void FireAction(InputAction.CallbackContext context,GameState.GameState state);

        public void Registration()
        {
            RegistFireEventMessage message = new RegistFireEventMessage(FireAction);
            Messenger.Publish(message);

            RegistMoveEventMessage moveEventMessage = new RegistMoveEventMessage(MoveAction);
            Messenger.Publish(moveEventMessage);
        }
    }
}