namespace HuntTheWumpus.GameEntities {
    public abstract class GameEntity {
        public int RoomNumber { get; protected set; }

        protected GameEntity(int roomNumber) {
            RoomNumber = roomNumber;
        }

        public abstract void PrintLocation();
    }
}