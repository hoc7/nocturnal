using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BoneGame.System
{
    public static class ResourceLoader
    {
        /// <summary>
        /// シーンごとのキャッシュのディクショナリー
        /// </summary>
        public static Dictionary<string, ResourceCache> SceneCacheDictionary = new Dictionary<string, ResourceCache>();

        /// <summary>
        /// 絶対消さないキャッシュのディクショナリー
        /// </summary>
        public static Dictionary<string, (int refCount, AsyncOperationHandle ope)> DontUnloadDictionary =
            new Dictionary<string, (int refCount, AsyncOperationHandle ope)>();

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            UnloadAll();
        }
#endif

        /// <summary>
        /// 指定されたアドレスのリソースをロードする
        /// </summary>
        /// <typeparam name="T">ロードするリソースの型</typeparam>
        /// <param name="address">ロードするリソースのアドレス名</param>
        /// <returns>ロードしたりソース</returns>
        public static UniTask<T> Load<T>(string address, bool dontUnload = false)
            where T : UnityEngine.Object
        {
            string sceneName =  BoneGame.System.SceneLoader.Instance.NowOpenScene;
            return Load<T>(address, sceneName, dontUnload);
        }

        /// <summary>
        /// 指定されたアドレスのリソースをロードする
        /// </summary>
        /// <typeparam name="T">ロードするリソースの型</typeparam>
        /// <param name="address">ロードするリソースのアドレス名</param>
        /// <returns>ロードしたりソース</returns>
        public static UniTask<T> Load<T>(AssetReference reference, bool dontUnload = false)
            where T : UnityEngine.Object
        {
            string sceneName = SceneLoader.Instance.NowOpenScene;
            return Load<T>(reference, sceneName, dontUnload);
        }

        /// <summary>
        /// 指定されたアドレスのリソースをロードする
        /// </summary>
        /// <typeparam name="T">ロードするリソースの型</typeparam>
        /// <param name="address">ロードするリソースのアドレス名</param>
        /// <returns>ロードしたりソース</returns>
        public static UniTask<T> Load<T>(AssetReference reference, string sceneName, bool dontUnload)
            where T : UnityEngine.Object
        {
            string address = reference.GetAddress();
            return Load<T>(address, sceneName, dontUnload);
        }

        /// <summary>
        /// 指定されたアドレスのリソースをロードする
        /// </summary>
        /// <typeparam name="T">ロードするリソースの型</typeparam>
        /// <param name="address">ロードするリソースのアドレス名</param>
        /// <returns>ロードしたりソース</returns>
        public static async UniTask<T> Load<T>(string address, string sceneName, bool dontUnload)
            where T : UnityEngine.Object
        {
            if (DontUnloadDictionary.ContainsKey(address))
            {
                return (T)DontUnloadDictionary[address].ope.Result;
            }

            if (sceneName != null)
            {

                if (!SceneCacheDictionary.ContainsKey(sceneName))
                {
                    SceneCacheDictionary[sceneName] = new ResourceCache();
                }

                if (SceneCacheDictionary[sceneName].ContainsKey(address))
                {
                    return (T) SceneCacheDictionary[sceneName].CacheDictionary[address].ope.Result;
                }
            }

            return await LoadResource<T>(address, sceneName, dontUnload);
        }


        /// <summary>
        /// Assetのロード処理
        /// </summary>
        /// <typeparam name="T">ロードするリソースの型</typeparam>
        /// <param name="address">ロードするリソースのアドレス名</param>
        /// <returns>ロードしたリソース</returns>
        private static async UniTask<T> LoadResource<T>(string address, string sceneName, bool dontUnload)
            where T : UnityEngine.Object
        {
            return await  Addressables.LoadAssetAsync<T>(address);
        }

        public static void UnloadAll()
        {
            // シーンごとのキャッシュ削除
            if (SceneCacheDictionary != null && SceneCacheDictionary.Any())
            {
                foreach (var sceneCache in SceneCacheDictionary)
                {
                    foreach (var kvp in sceneCache.Value.CacheDictionary)
                    {
                        Debug.Log(kvp.Value.ope.DebugName + " is Release!");

                        for (int i = 0; i < kvp.Value.refCount; i++)
                        {
                            Addressables.Release(kvp.Value.ope);
                        }
                    }
                }

                SceneCacheDictionary.Clear();
            }

            // DontUnloadのキャッシュ削除
            if (DontUnloadDictionary != null && DontUnloadDictionary.Any())
            {
                foreach (var dontUnloadCache in DontUnloadDictionary)
                {
                    Debug.Log(dontUnloadCache.Value.ope.DebugName + " is Release!");
                    for (int i = 0; i < dontUnloadCache.Value.refCount; i++)
                    {
                        Addressables.Release(dontUnloadCache.Value.ope);
                    }
                }

                DontUnloadDictionary.Clear();
            }
        }
        

    }
}