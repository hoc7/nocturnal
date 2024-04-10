using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoneGame.System
{
    public class RegistFireEventMessage
    {
        public Action<InputAction.CallbackContext > Event;

        public RegistFireEventMessage(Action<InputAction.CallbackContext > @event)
        {
            Event = @event;
        }
    }
    
    public class RegistMoveEventMessage
    {
        public Action<InputAction.CallbackContext > Event;

        public RegistMoveEventMessage(Action<InputAction.CallbackContext > @event)
        {
            Event = @event;
        }
    }
}