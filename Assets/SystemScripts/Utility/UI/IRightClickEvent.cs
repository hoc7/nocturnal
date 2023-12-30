namespace BoneGame.System.UI
{
    public interface IRightClickEvent
    {
        public void SetEvent();

        public void RemoveEvent()
        {
            RightClickHandler.Instance.RemoveEvent();
        }
    }
}