using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hunt_the_wumpus
{
    class Program
    {
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

        static void Main()
        {
            Thread.Sleep(500);
            Console.WriteLine(HUNT_ASCII);
            Thread.Sleep(500);
            Console.WriteLine(THE_ASCII);
            Thread.Sleep(500);
            Console.WriteLine(WUMPUS_ASCII);
            Console.ReadLine();
        }
    }
}