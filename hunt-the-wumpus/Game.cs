using System;
using System.Collections.Generic;

namespace HuntTheWumpus {
    public class Game {
        private readonly ISet<string> _acceptableResponses = new HashSet<string> { "Y", "N" };
        private Map _map;

        internal Game() {
            _map = new Map();
        }

        public void Run() {
            string playResponse;
            do {
                Play();
                playResponse = GetValidResponse(Msg.PlayPrompt, _acceptableResponses);
                if (playResponse == "Y")
                    ResetMap();
            } while (playResponse == "Y");
        }

        private void Play() {
            EndState endState;

            do {
                _map.Update();
                string command = GetValidResponse(Msg.ActionPrompt, new HashSet<string> { "S", "M", "Q" });
                endState = _map.GetEndState(command);
            } while (!endState.IsGameOver);

            endState.Print();
        }

        private void ResetMap() {
            string setupResponse = GetValidResponse(Msg.SetupPrompt, _acceptableResponses);
            if (setupResponse == "Y")
                _map.Reset();
            else
                _map = new Map();
        }

        /// <summary>
        ///     Gets an acceptable response to a question.
        /// </summary>
        /// <param name="question">The question to ask</param>
        /// <param name="acceptableResponses">acceptable responses to the question (all assumed to be uppercase)</param>
        /// <returns>an acceptable response</returns>
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