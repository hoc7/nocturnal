using BoneGame.Data;
using BoneGame.Event;
using UnityEngine;

namespace BoneGame.Nocturnal.GameData
{
    [CreateAssetMenu(fileName = "PurposeStarMaster", menuName = "PurposeStarMaster作成", order = 100)]
    public class PurposeStarMaster : PurposeMasterBase
    {
        public int HitId;

        public override bool Check(int hitId)
        {
            return HitId == hitId;
        }

        public override string GetText()
        {
            return string.Format("{0}を撮影する。", Name);
        }
    }
}