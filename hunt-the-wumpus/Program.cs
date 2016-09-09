using System;
using System.Threading;

namespace hunt_the_wumpus {
    internal class Program {
        private const string HUNT_ASCII = @"
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
        private const string THE_ASCII = @"
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
        private const string WUMPUS_ASCII = @"
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
            DisplayLogo();
            try {
                var game = new Game();
                game.Run();
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        private static void DisplayLogo() {
            Thread.Sleep(500);
            Console.WriteLine(HUNT_ASCII);
            Thread.Sleep(500);
            Console.WriteLine(THE_ASCII);
            Thread.Sleep(500);
            Console.WriteLine(WUMPUS_ASCII);
        }
    }
}