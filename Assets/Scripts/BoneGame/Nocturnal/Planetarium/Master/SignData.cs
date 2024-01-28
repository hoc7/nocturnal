using System;
using System.Collections.Generic;
using System.Linq;

namespace BoneGame.Nocturnal.Planetarium
{
    [Serializable]
    public class SignData
    {
        public string SignKey;
        public string EngName;
        public string JapName;
        public int SignId;
        public List<HipLine> _lines = new List<HipLine>();

        /// <summary>
        /// 星座を構成する星の一覧を取得
        /// </summary>
        /// <returns></returns>
        public List<int> GetStarsInSign()
        {
            return _lines.Select(_ => _.sttHipId).Union(_lines.Select(_ => _.endHipId)).ToList();
        }

        
        /// <summary>
        /// 引数の中に星座が含まれているかどうか
        /// </summary>
        /// <param name="stars"></param>
        /// <returns></returns>
        public bool IsContainSigtn(List<int> stars)
        {
            return GetStarsInSign().All(stars.Contains);
        }
 
        public SignData(string signKey)
        {
            SignKey = signKey;
        }
    }
}