using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoneGame.Message;
using NPOI.SS.Formula.Functions;
using Sirenix.OdinInspector;
using UnityEditor;

namespace BoneGame.Dialogue
{
    [Serializable]
    public class Talk : DialogueEntity
    {
#if UNITY_EDITOR
        [HorizontalGroup("Actor/CharacterButton")]
        [Button("ヨイヤミ")]
        private void ActorSetting()
        {
            Actor = FindActor("ヨイヤミ");
        }

        [HorizontalGroup("Actor/CharacterButton")]
        [Button("アオゾラ")]
        private void ActorSettingAozora()
        {
            Actor = FindActor("アオゾラ");
        }

        private Actor FindActor(string actorName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("t:");
            builder.Append(typeof(Actor));
            var guids = UnityEditor.AssetDatabase.FindAssets(builder.ToString());

            List<Actor> masters = new List<Actor>();
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var objects = AssetDatabase.LoadAllAssetsAtPath(path).ToList();
                masters.AddRange(objects.OfType<Actor>().ToList());
            }

            return masters.FirstOrDefault(_ => _.Name == actorName);
        }
#endif

        [VerticalGroup("Actor")] public Actor Actor;
        [MultiLineProperty(4)] public string Text;

        public override void SendMessage()
        {
            DialogueTextMessage message = new DialogueTextMessage(this);
            Messenger.Publish(message);
        }
    }
}