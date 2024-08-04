using System.Collections.Generic;
using System.IO;
using BoneGame.System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BoneGame.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData", order = 0)]
    public class DialogueData : MasterDataScriptableObject
    {
        [LabelText("会話パート内容")]
        [OdinSerialize]
        [InlineProperty]
        public List<DialogueEntity> Dialogues = new List<DialogueEntity>();


        [Button("会話パート追加")]
        private void AddDialogue()
        {
            Dialogues.Add(new Talk());
        }
        
        
        [SerializeField] private TextAsset CSV;
        [Button("csvデータで上書き")]
        public void CreateMasterFromCSV()
        {
            List<string[]> csvDatas = new List<string[]>();
            StringReader reader = new StringReader(CSV.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                csvDatas.Add(line.Split(","));
            }
            var master = this;

            this.Dialogues = new List<DialogueEntity>();
            foreach (var csv in csvDatas)
            {
                Talk entity = new Talk();
                if (csv[0] == "演出"||csv[0]=="操作") continue;
                entity.Actor = entity.FindActor(csv[0]);
                entity.Text = csv[1];
                master.Dialogues.Add(entity);
            }
      
        }
    }
}