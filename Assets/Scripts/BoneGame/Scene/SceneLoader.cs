using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoneGame.System
{
    /// <summary>
    /// シーンをロードする仕組み
    /// </summary>
    public class SceneLoader
    {
        private static SceneLoader _instance;

        public static SceneLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneLoader();
                }
                return _instance;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            _instance = null;
        }

        private SceneLoader()
        {
            Event = null;
        }
        
        private SceneStartEventBase Event;

        public string NowOpenScene => SceneManager.GetActiveScene().name;

        public async UniTask MoveScene(string sceneName, SceneStartEventBase eventBase = null)
        {
            await SceneManager.LoadSceneAsync(sceneName,
                new LoadSceneParameters(LoadSceneMode.Single));
            Event = eventBase;
            SceneInitialize(SceneManager.GetActiveScene(),LoadSceneMode.Single);
        }


        private void SceneInitialize(Scene scene, LoadSceneMode mode)
        {
            if(Event == null) return;
            foreach (var go in scene.GetRootGameObjects())
            {
                if (go.TryGetComponent<ISceneInitializer>(out ISceneInitializer initializer))
                {
                    initializer.Initialization(Event);
                }
            }
        }
    }
}