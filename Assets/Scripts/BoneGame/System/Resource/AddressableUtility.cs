using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using AssetReference = UnityEngine.AddressableAssets.AssetReference;


namespace BoneGame.System
{
    /// <summary>
    /// Addressableに関わるUtility
    /// </summary>
    public static class AddressableUtility
    {
        public static bool Exists(string address)
        {
            foreach (var l in Addressables.ResourceLocators)
            {
                IList<IResourceLocation> locs;
                if (l.Locate(address, typeof(AudioClip), out locs))
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }

            return false;
        }

        /// <summary>
        /// アセットリファレンスからアドレスを取得するメソッド
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static string GetAddress(this AssetReference reference)
        {
            string address = null;

            foreach (var locator in Addressables.ResourceLocators)
            {

                IList<IResourceLocation> locs;
                if (locator.Locate(reference.RuntimeKey, null, out locs))
                {
                    address = locs[0].PrimaryKey;

                    return address;
                }
            }

            // 無いと思うけど無かったらnullを返す
            return address;
        }
    }
}