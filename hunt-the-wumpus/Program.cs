using System;
using System.Threading;

namespace HuntTheWumpus {
    internal class Program {
        private const string HuntAscii = @"
 ██░ ██  █    ██  ███▄    █ ▄▄▄█████▓                  
▓██░ ██▒ ██  ▓██▒ ██ ▀█   █ ▓  ██▒ ▓▒                  
▒██▀▀██░▓██  ▒██░▓██  ▀█ ██▒▒ ▓██░ ▒░                  
░▓█ ░██ ▓▓█  ░██░▓██▒  ▐▌██▒░ ▓██▓ ░                   
░▓█▒░██▓▒▒█████▓ ▒██░   ▓██░  ▒██▒ ░                   
 ▒ ░░▒░▒░▒▓▒ ▒ ▒ ░ ▒░   ▒ ▒   ▒ ░░                     
 ▒ ░▒░ ░░░▒░ ░ ░ ░ ░░   ░ ▒░    ░                      
 ░  ░░ ░ ░░░ ░ ░    ░   ░ ░   ░                        
 ░  ░  ░   ░              ░                            
";
        private const string TheAscii = @"
▄▄▄█████▓ ██░ ██ ▓█████                                
▓  ██▒ ▓▒▓██░ ██▒▓█   ▀                                
▒ ▓██░ ▒░▒██▀▀██░▒███                                  
░ ▓██▓ ░ ░▓█ ░██ ▒▓█  ▄                                
  ▒██▒ ░ ░▓█▒░██▓░▒████▒                               
  ▒ ░░    ▒ ░░▒░▒░░ ▒░ ░                               
    ░     ▒ ░▒░ ░ ░ ░  ░                               
  ░       ░  ░░ ░   ░                                  
          ░  ░  ░   ░  ░                               
";
        private const string WumpusAscii = @"
 █     █░█    ██  ███▄ ▄███▓ ██▓███   █    ██   ██████ 
▓█░ █ ░█░██  ▓██▒▓██▒▀█▀ ██▒▓██░  ██▒ ██  ▓██▒▒██    ▒ 
▒█░ █ ░█▓██  ▒██░▓██    ▓██░▓██░ ██▓▒▓██  ▒██░░ ▓██▄   
░█░ █ ░█▓▓█  ░██░▒██    ▒██ ▒██▄█▓▒ ▒▓▓█  ░██░  ▒   ██▒
░░██▒██▓▒▒█████▓ ▒██▒   ░██▒▒██▒ ░  ░▒▒█████▓ ▒██████▒▒
░ ▓░▒ ▒ ░▒▓▒ ▒ ▒ ░ ▒░   ░  ░▒▓▒░ ░  ░░▒▓▒ ▒ ▒ ▒ ▒▓▒ ▒ ░
  ▒ ░ ░ ░░▒░ ░ ░ ░  ░      ░░▒ ░     ░░▒░ ░ ░ ░ ░▒  ░ ░
  ░   ░  ░░░ ░ ░ ░      ░   ░░        ░░░ ░ ░ ░  ░  ░  
    ░      ░            ░               ░           ░  
";

        private static void Main() {
            Console.SetWindowSize(80, 50);
            DisplayLogo();
            try {
                var game = new Game();
                game.Run();
                Console.ReadLine();
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        private static void DisplayLogo() {
            Thread.Sleep(500);
            Console.WriteLine(HuntAscii);
            Thread.Sleep(500);
            Console.WriteLine(TheAscii);
            Thread.Sleep(500);
            Console.WriteLine(WumpusAscii);
        }
    }
}