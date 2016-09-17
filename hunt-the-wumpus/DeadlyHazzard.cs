namespace HuntTheWumpus {
    public abstract class DeadlyHazzard : Hazzard {
        public abstract EndState GetEndState(int playerRoomNumber);
    }
}