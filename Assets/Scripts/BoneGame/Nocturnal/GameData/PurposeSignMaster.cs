using System.Collections.Generic;
using BoneGame.Data;
using BoneGame.Event;
using BoneGame.Nocturnal.Data;
using BoneGame.Nocturnal.Planetarium;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BoneGame.Nocturnal.GameData
{
    [CreateAssetMenu(fileName = "PurposeSignMaster", menuName = "PurposeSignMaster作成", order = 100)]
    public class PurposeSignMaster: PurposeMasterBase
    {
            public int SignId;

            public override bool Check(int signId)
            {
                return SignId == signId;
            }

            public override string GetText()
            {
                SignData signData = MasterDataHolder.Instance.GetSine(SignId);
                return string.Format("{0}座を撮影する。", signData.JapName);
            }


#if UNITY_EDITOR


        [SerializeField] private SignMasters Masters;
        
        [Button("マスターの一括作成")]
        public void CreateMasters()
        {
            List<SignData> signDatas = Masters.GetAllSignData();
            foreach (SignData sign in signDatas)
            {
                PurposeSignMaster master = ScriptableObject.CreateInstance<PurposeSignMaster>();
                master.Id = sign.SignId;
                master.SignId = sign.SignId;
                master.Name = sign.JapName;
                string path = "Assets/MasterData/PurposeMaster/PurposeSign/" + sign.SignId +"_"+ sign.JapName +"座を探せ.asset";
                AssetDatabase.CreateAsset(master, path);
            }
        }
        
#endif
            
    }
}