namespace HuntTheWumpus {
    public class Msg {
        public const string HuntAscii = @"
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
        public const string TheAscii = @"
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
        public const string WumpusAscii = @"
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
        public const string ActionPrompt = "Shoot, Move or Quit(S - M - Q)? ";
        public const string PlayPrompt = "Play again? (Y-N)";
        public const string SetupPrompt = "Same Setup? (Y-N)";
        public const string NumOfRoomsToShootPrompt = "No. or rooms (0-5)?";
        public const string RoomNumPrompt = "Room #?";

        public const string PitWarning = "I feel a draft!";
        public const string WumpusWarning = "I Smell a Wumpus.";
        public const string BatWarning = "Bats nearby!";

        public const string BatSnatch = "Zap--Super Bat snatch! Elsewhereville for you!";
        public const string WumpusBump = "...Oops! Bumped a wumpus!";

        public const string FellInPit = "YYYIIIIEEEE... fell in a pit!";
        public const string WumpusGotYou = "Tsk tsk tsk - wumpus got you!";
        public const string LoseMessage = "Ha ha ha - you lose!";
        public const string WinMessage = "Aha! You got the Wumpus!\nHee hee hee - the Wumpus'll getcha next time!!";
    }
}