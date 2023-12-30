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
            get => _instance;
            set => _instance = value;
        }

        private SceneLoader()
        {
            SceneManager.sceneLoaded -= SceneInitialize;
            SceneManager.sceneLoaded -= SceneInitialize;
            SceneManager.sceneLoaded -= SceneInitialize;
            SceneManager.sceneLoaded -= SceneInitialize;
            
            SceneManager.sceneLoaded += SceneInitialize;
            Event = null;
        }
        
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration )]
        private static void Init()
        {
            _instance = new SceneLoader();
        }


        public string NowOpenScene;
        private SceneStartEventBase Event;
        

        public void MoveScene(string sceneName, SceneStartEventBase eventBase = null)
        {
            var scene = SceneManager.LoadScene(sceneName,
                new LoadSceneParameters(LoadSceneMode.Single));
            NowOpenScene = sceneName;
            Event = eventBase;
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