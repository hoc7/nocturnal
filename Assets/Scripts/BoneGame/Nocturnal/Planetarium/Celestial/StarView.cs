using BoneGame.Data;
using BoneGame.Event;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium
{
    public class StarView : MonoBehaviour
    {
        public int HitId;
        public void Set(StarData hipData)
        {
            HitId = hipData.HitId;
            float baseScale = MasterDataHolder.Instance.GetStarScale();
            float scale = (5f + (8 - hipData.Magnitude) * 8.5f) * (baseScale / 5000f);
            this.transform.position = hipData.GetPosition(baseScale);
            this.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}