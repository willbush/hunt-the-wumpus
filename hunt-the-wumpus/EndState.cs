using System;

namespace HuntTheWumpus {
    /// <summary>
    ///     This class basically servies as a immutable tuple that allows me to pass
    ///     the game end state in a functional style without having to introduce mutable fields
    ///     or break control of flow with system exits and other lazy hacks.
    /// </summary>
    public class EndState {
        public bool IsGameOver { get; }
        private string GameOverMessage { get; }

        internal EndState() {}

        internal EndState(bool isGameOver, string gameOverMessage) {
            IsGameOver = isGameOver;
            GameOverMessage = gameOverMessage;
        }

        public void Print() {
            Console.WriteLine();
            if (string.IsNullOrEmpty(GameOverMessage)) return;

            Console.WriteLine(GameOverMessage);
            Console.WriteLine();
        }
    }
}