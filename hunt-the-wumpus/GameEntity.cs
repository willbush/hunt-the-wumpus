namespace HuntTheWumpus {
    public abstract class GameEntity {
        public int RoomNumber { get; set; }
        public abstract void PrintLocation();
    }
}