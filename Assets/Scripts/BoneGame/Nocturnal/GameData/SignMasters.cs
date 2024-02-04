using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BoneGame.Nocturnal.Planetarium;
using NPOI.SS.Formula.Functions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BoneGame.Nocturnal.Data
{
    /// <summary>
    /// 星座マスター
    /// </summary>
  
    public class SignMasters : MasterDataScriptableObject
    {
        [SerializeField] private TextAsset NameCsv;
        [SerializeField] private List<TextAsset> HipData;
        [SerializeField] private List<SignData> Signs = new List<SignData>();
        public SignData GetSignData(int signId)
        {
            return Signs.FirstOrDefault(_ => _.SignId == signId);
        }

        public List<SignData> GetAllSignData()
        {
            return Signs;
        }

        /// <summary>
        /// 集められた星の集団からSignがあれば
        /// </summary>
        /// <param name="starDatas"></param>
        /// <returns></returns>
        public List<SignData> GetContainSigns(List<int> hitIds)
        {
            return Signs.Where(_ => _.IsContainSigtn(hitIds)).ToList();
        }
        
#if UNITY_EDITOR

        [Button("名前データの作成・マスターへの入力")]
        public void CreateNames()
        {
            StringReader sr = new StringReader(NameCsv.text);
            while (true)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                else
                {
                    string[] dataArr = line.Split(',');
                    try
                    {
                        string key = dataArr[1];
                        string engName = dataArr[2];
                        string japName = dataArr[3];

                        var sign = Signs.FirstOrDefault(_ => _.SignKey == key);
                        sign.EngName = engName;
                        sign.JapName = japName;
                    }
                    catch
                    {
                        
                    }
                }
            }
        }
        
        
        
        [Button("星座線データの生成")]
        public void CreateMaster()
        {
            foreach (var csv in HipData)
            {
                var createdData = HipDataFactory.CreateHipLines(csv);
                foreach (HipLine hipLine in createdData)
                {
                    var signData = Signs.FirstOrDefault(_ => _.SignKey == hipLine.signKey);
                    if (signData == null)
                    {
                        signData = new SignData(hipLine.signKey);
                        Signs.Add(signData);
                    }

                    if (!signData._lines.Any(_ => _.sttHipId == hipLine.sttHipId && _.endHipId == hipLine.endHipId))
                    {
                        signData._lines.Add(hipLine);
                    }
                }
            }
        }

        [Button("星座Idの設定")]
        public void SetIds()
        {
            int index = 1;
            foreach (var sign in Signs)
            {
                sign.SignId = index;
                index++;
            }
        }
    }
#endif
}