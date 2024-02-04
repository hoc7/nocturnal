using System;
using BoneGame.Message;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace BoneGame.Nocturnal.Planetarium.Book
{

    public class CharacterTabButton : MonoBehaviour
    {
        private void Reset()
        {
            button = this.GetComponent<Button>();
        }

        [SerializeField]
        private Button button; // assign in inspector
        public CharacterTab characterTab; // assign in inspector

        private void Start()
        {
            button.OnClickAsObservable()
                .Subscribe(_ => SendMessage()).AddTo(this);
        }

        private void SendMessage()
        {
            CharacterTabSelectMessage message = new CharacterTabSelectMessage(this.characterTab);
            Messenger.Publish(message);
        }
    }
}