using BoneGame.Message;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BoneGame.Nocturnal.Planetarium.Book
{
    public class TypeTabButton : MonoBehaviour
    {
        [SerializeField]
        private Button button; // assign in inspector
        [FormerlySerializedAs("characterTab")] public TypeTab typeTab; // assign in inspector

        private void Start()
        {
            button.OnClickAsObservable()
                .Subscribe(_ => SendMessage()).AddTo(this);
        }

        private void SendMessage()
        {
            TypeTabSelectMessage message = new TypeTabSelectMessage(this.typeTab);
            Messenger.Publish(message);
        }
    }
}