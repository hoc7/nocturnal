using BoneGame.Event;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium
{
    public class StarView : MonoBehaviour
    {
        public void Set(StarData hipData)
        {
            float scale = 3f + 1f / hipData.Magnitude;
            this.transform.position = hipData.GetPosition;
            this.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}