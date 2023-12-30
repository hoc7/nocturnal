namespace BoneGame.Event
{
    public class EventClearMessage
    {
        public int ClearEventId;

        public EventClearMessage(int clearEventId)
        {
            ClearEventId = clearEventId;
        }
    }
}