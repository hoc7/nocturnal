using System;
using UnityEngine;

namespace BoneGame.Event
{
    public abstract class PurposeMasterBase : MasterDataScriptableObject
    {
        public abstract bool Check(int id);

        public abstract string GetText();
    }
}