namespace BoneGame.System.GameState
{
    public enum GameState
    {
        Idle,
        Moving,
        Talking
    }

    public class GameStateModel
    {
        public GameState CurrentState { get; private set; } = GameState.Idle;

        public void ChangeState(GameState state)
        {
            CurrentState = state;
        }
    }
}