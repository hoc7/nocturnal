using System;
using System.Collections.Generic;
using BoneGame.Message;
using BoneGame.System.GameState;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoneGame.System
{
    public class InputBehaviour : MonoBehaviour
    {
        private GameStateModel StateModel;
        private List<Action<InputAction.CallbackContext,GameState.GameState>> OnMoveEvent = new List<Action<InputAction.CallbackContext,GameState.GameState>>();
        private List<Action<InputAction.CallbackContext,GameState.GameState>> OnFireEvent = new List<Action<InputAction.CallbackContext,GameState.GameState>>();
        private bool CanMove = false;

        private void Awake()
        {
            StateModel = new GameStateModel();
            Messenger.Receive<RegistMoveEventMessage>().Subscribe(_ => { OnMoveEvent.Add(_.Event); }).AddTo(this);

            Messenger.Receive<RegistFireEventMessage>().Subscribe(_ => { OnFireEvent.Add(_.Event); }).AddTo(this);

            Messenger.Receive<StateChangeMessage>().Subscribe(_ =>
            {
                StateModel.ChangeState(_.GameState);
            }).AddTo(this);
            
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            foreach (var ev in OnMoveEvent)
            {
                ev(context,StateModel.CurrentState);
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            foreach (var fire in OnFireEvent)
            {
                fire(context,StateModel.CurrentState);
            }
        }
    }
}