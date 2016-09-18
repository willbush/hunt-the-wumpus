using System;
using System.Threading;

namespace HuntTheWumpus {
    /// <summary>
    ///     The entry point of the application. Game logo is displayed and the game is started.
    /// </summary>
    internal static class Program {
        private static void Main(string[] arguments) {
            Console.SetWindowSize(height: 50, width: 80);
            DisplayLogo();

            try {
                bool isCheatMode = arguments.Length > 0 && arguments[0].ToLower() == "cheat";
                var game = new Game(isCheatMode);
                game.Run();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                Console.Write("Press Enter to close.");
                Console.ReadLine();
            }
        }

        private static void DisplayLogo() {
            Thread.Sleep(500);
            Console.WriteLine(Message.HuntAscii);
            Thread.Sleep(500);
            Console.WriteLine(Message.TheAscii);
            Thread.Sleep(500);
            Console.WriteLine(Message.WumpusAscii);
        }
    }
}