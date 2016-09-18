using System;
using System.Collections.Generic;

namespace HuntTheWumpus {
    /// <summary>
    ///     Game class has the main game loop and handles the flow of control for playing and reseting the game.
    /// </summary>
    public class Game {
        private readonly ISet<string> _acceptableResponses = new HashSet<string> { "Y", "N" };
        private Map _map;

        internal Game() {
            _map = new Map();
        }

        /// <summary>
        ///     Runs the game as long as the player wants,
        ///     and if the player wants, will reset the game.
        /// </summary>
        public void Run() {
            string playResponse;
            do {
                Play();
                playResponse = GetValidResponse(Message.PlayPrompt, _acceptableResponses);
                if (playResponse == "Y")
                    ResetMap();
            } while (playResponse == "Y");
        }

        // Main game loop that runs until the game is over.
        private void Play() {
            EndState endState;

            do {
                _map.Update();
                string command = GetValidResponse(Message.ActionPrompt, new HashSet<string> { "S", "M", "Q" });
                endState = _map.GetEndState(command);
            } while (!endState.IsGameOver);

            endState.Print();
        }

        private void ResetMap() {
            string setupResponse = GetValidResponse(Message.SetupPrompt, _acceptableResponses);
            if (setupResponse == "Y")
                _map.Reset();
            else
                _map = new Map();
        }

        private static string GetValidResponse(string question, ICollection<string> acceptableResponses) {
            string response;
            do {
                Console.Write(question);
                response = Console.ReadLine()?.Trim().ToUpper();
            } while (!acceptableResponses.Contains(response));

            return response;
        }
    }
}