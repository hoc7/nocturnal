namespace BoneGame.SaveSystem
{
    public class SaveDataDeleteMessage
    {
        public int SaveId;

        public SaveDataDeleteMessage(int saveId)
        {
            SaveId = saveId;
        }
    }
}