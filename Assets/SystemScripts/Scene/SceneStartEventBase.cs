
using Newtonsoft.Json.Linq;

namespace BoneGame.System
{
    public class SceneStartEventBase
    {
        public JObject JObject;

        public SceneStartEventBase(JObject jObject)
        {
            JObject = jObject;
        }
    }
}