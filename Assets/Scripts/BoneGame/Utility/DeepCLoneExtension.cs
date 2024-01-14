
using Newtonsoft.Json;

namespace BoneGame.System
{


    public static class DeepCloneExtension
    {
        public static T DeepClone<T>(this T obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}