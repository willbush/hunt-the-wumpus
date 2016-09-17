namespace HuntTheWumpus.GameEntities {
    public abstract class Hazzard : GameEntity {
        public abstract void PrintHazardWarning();
        public abstract void Update(Player player);
    }
}