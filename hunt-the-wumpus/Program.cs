using System;
using System.Threading;

namespace HuntTheWumpus {
    internal class Program {
        private static void Main() {
            Console.SetWindowSize(80, 50);
            DisplayLogo();

            try {
                var game = new Game();
                game.Run();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                Console.Write("Press Enter to close.");
                Console.ReadLine();
            }
        }

        private static void DisplayLogo() {
            Thread.Sleep(500);
            Console.WriteLine(Msg.HuntAscii);
            Thread.Sleep(500);
            Console.WriteLine(Msg.TheAscii);
            Thread.Sleep(500);
            Console.WriteLine(Msg.WumpusAscii);
        }
    }
}