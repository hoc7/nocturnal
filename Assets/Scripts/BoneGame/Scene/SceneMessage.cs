namespace BoneGame.System
{
    public class SceneMessage
    {
        public string NextSceneName;
        public SceneStartEventBase SceneStartEventBase;

        public SceneMessage(SceneStartEventBase eventBase,string nextSceneName)
        {
            SceneStartEventBase = eventBase;
            NextSceneName = nextSceneName;
        }
    }
}