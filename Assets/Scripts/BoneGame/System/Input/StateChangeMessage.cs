namespace BoneGame.System
{
    public class StateChangeMessage
    {
        public GameState.GameState GameState;

        public StateChangeMessage(GameState.GameState gameState)
        {
            this.GameState = gameState;
        }
    }
}