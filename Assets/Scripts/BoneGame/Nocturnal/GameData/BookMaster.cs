using System;
using System.Collections.Generic;
using BoneGame.Nocturnal.Data;
using BoneGame.Nocturnal.Planetarium;
using BoneGame.Nocturnal.Planetarium.Book;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BoneGame.Nocturnal.GameData
{
    [CreateAssetMenu(fileName = "BookMaster", menuName = "BookMaster作成", order = 100)]
    public class BookMaster : MasterDataScriptableObject
    {
        public TypeTab TypeTab;
        public Sprite Image;
        [Multiline(15)]
        public string Information;
        
#if UNITY_EDITOR


        [SerializeField] private SignMasters Masters;
        
        [Button("マスターの一括作成")]
        public void CreateMasters()
        {
            List<SignData> signDatas = Masters.GetAllSignData();
            foreach (SignData sign in signDatas)
            {
                BookMaster master = ScriptableObject.CreateInstance<BookMaster>();
                master.Id = sign.SignId;
                master.Name = sign.JapName + "座";
                string path = "Assets/MasterData/BookMaster/" + sign.SignId +"_"+ master.Name +"の解説.asset";
                AssetDatabase.CreateAsset(master, path);
            }
        }
        
#endif
    }
}