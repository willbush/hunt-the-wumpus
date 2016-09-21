namespace HuntTheWumpus.GameEntities {
    public abstract class DeadlyHazard : Hazard {
        protected DeadlyHazard(int roomNumber) : base(roomNumber) {}
        public abstract EndState DetermineEndState(int playerRoomNumber);
    }
}