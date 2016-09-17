namespace HuntTheWumpus.GameEntities {
    public abstract class DeadlyHazzard : Hazzard {
        public abstract EndState GetEndState(int playerRoomNumber);
    }
}