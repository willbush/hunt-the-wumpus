namespace HuntTheWumpus.GameEntities {
    public abstract class DeadlyHazard : Hazard {
        public abstract EndState DetermineEndState(int playerRoomNumber);
    }
}