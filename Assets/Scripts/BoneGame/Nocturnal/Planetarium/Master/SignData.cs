using System;
using System.Collections.Generic;

namespace BoneGame.Nocturnal.Planetarium
{
    [Serializable]
    public class SignData
    {
        public string SignKey;
        public int SignId;
        public List<HipLine> _lines = new List<HipLine>();

        public SignData(string signKey)
        {
            SignKey = signKey;
        }
    }
}