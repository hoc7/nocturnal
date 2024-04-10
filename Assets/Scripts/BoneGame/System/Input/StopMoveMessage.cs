namespace BoneGame.System
{
    public class InputMoveStateChangeMessage
    {
        public bool canMove;

        public InputMoveStateChangeMessage(bool canMove)
        {
            this.canMove = canMove;
        }
    }
}