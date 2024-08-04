using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoneGame.System
{
    public class RegistFireEventMessage
    {
        public Action<InputAction.CallbackContext,GameState.GameState > Event;

        public RegistFireEventMessage(Action<InputAction.CallbackContext,GameState.GameState > @event)
        {
            Event = @event;
        }
    }
    
    public class RegistMoveEventMessage
    {
        public Action<InputAction.CallbackContext,GameState.GameState > Event;

        public RegistMoveEventMessage(Action<InputAction.CallbackContext,GameState.GameState > @event)
        {
            Event = @event;
        }
    }
}