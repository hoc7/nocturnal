using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BoneGame.System
{
    /// <summary>
    /// 一度読んだリソースのキャッシュをするクラス
    /// </summary>
    public class ResourceCache
    {
        /// <summary>
        /// キャッシュのディクショナリー
        /// </summary>
        public Dictionary<string, (int refCount, AsyncOperationHandle ope)> CacheDictionary =
            new Dictionary<string, (int refCount, AsyncOperationHandle ope)>();

        public UnityEngine.Object obj;

        /// <summary>
        /// 参照カウントを取得する
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public int GetRefCount(string address)
        {
            if (CacheDictionary.ContainsKey(address))
            {
                return CacheDictionary[address].refCount;
            }

            return 0;
        }

        /// <summary>
        /// キーがあるかチェックする
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool ContainsKey(string address)
        {
            if (CacheDictionary.ContainsKey(address)) return true;
            return false;
        }
    }
}