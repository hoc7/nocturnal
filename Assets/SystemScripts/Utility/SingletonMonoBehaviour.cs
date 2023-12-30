using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BoneGame.System
{
    public abstract class SingletonMonoBehaviour<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
    {

        /// <summary>
        /// DontDestroyOnLoadを呼ぶか否か
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsDontDestroyOnLoad()
        {
            return false;
        }

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T) FindObjectOfType(t);
                    if (instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).ToString());
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            // 他のGameObjectにアタッチされているか調べる.
            // アタッチされている場合は破棄する.
            if (this != Instance)
            {
                Destroy(this);
                Debug.LogError(
                    typeof(T) +
                    " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                    " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
                return;
            }

            // シーン遷移時の破棄を行う・行わない
            if (this.IsDontDestroyOnLoad())
            {
                // DontDestroyOnLoadを呼ぶ設定になっていれば呼ぶ
                DontDestroyOnLoad(this.gameObject);
            }

            Application.quitting += Quitting;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Application.quitting -= Quitting;
        }

        private static void Quitting()
        {
            instance = null;
        }

    }
}