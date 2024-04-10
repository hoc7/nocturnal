using System;
using System.Collections.Generic;
using BoneGame.Message;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoneGame.System
{
    public class InputBehaviour : MonoBehaviour
    {
        private List<Action<InputAction.CallbackContext>> OnMoveEvent = new List<Action<InputAction.CallbackContext>>();
        private List<Action<InputAction.CallbackContext>> OnFireEvent = new List<Action<InputAction.CallbackContext>>();
        private bool CanMove = false;

        private void Awake()
        {
            Messenger.Receive<RegistMoveEventMessage>().Subscribe(_ => { OnMoveEvent.Add(_.Event); }).AddTo(this);

            Messenger.Receive<RegistFireEventMessage>().Subscribe(_ => { OnFireEvent.Add(_.Event); }).AddTo(this);

            Messenger.Receive<InputMoveStateChangeMessage>().Subscribe(_ =>
            {
                CanMove = _.canMove;
            }).AddTo(this);
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            if (!CanMove) return;
            foreach (var ev in OnMoveEvent)
            {
                ev(context);
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            foreach (var fire in OnFireEvent)
            {
                fire(context);
            }
        }
    }
}