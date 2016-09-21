namespace HuntTheWumpus.GameEntities {
    public abstract class Hazard : GameEntity {
        protected Hazard(int roomNumber) : base(roomNumber) {}
        public abstract void PrintHazardWarning();
    }
}