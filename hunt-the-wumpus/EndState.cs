using System;

namespace HuntTheWumpus {
    public class EndState {
        public bool IsGameOver { get; }
        public string GameOverMessage { get; }

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