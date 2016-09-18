namespace HuntTheWumpus.GameEntities {
    public abstract class Hazard : GameEntity {
        public abstract void PrintHazardWarning();

        /// <summary>
        ///     Every hazard needs to be able to update some state
        ///     or print things out depending on the players position.
        /// </summary>
        public abstract void Update(Map map);
    }
}