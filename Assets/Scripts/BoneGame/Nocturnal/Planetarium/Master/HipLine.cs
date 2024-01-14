using System;
using UnityEngine.Serialization;

namespace BoneGame.Nocturnal.Planetarium
{
    [Serializable]
    public class HipLine
    {
         public string signKey;
        public int sttHipId;
        public int endHipId;
        
        public HipLine(string _name, int _sttId, int _endId)
        {
            signKey = _name;
            sttHipId = _sttId;
            endHipId = _endId;
        }

        public HipLine()
        {
            
        }
    }
}