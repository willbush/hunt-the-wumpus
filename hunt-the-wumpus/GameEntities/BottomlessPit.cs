using System;

namespace HuntTheWumpus.GameEntities {
    public class BottomlessPit : DeadlyHazzard {
        public override void PrintLocation() {
            Console.WriteLine($"Bottomless pit in room {RoomNumber}");
        }

        public override void PrintHazardWarning() {
            Console.WriteLine(Msg.PitWarning);
        }

        public override void Update(Player player) {}

        public override EndState GetEndState(int playerRoomNumber) {
            return playerRoomNumber == RoomNumber
                ? new EndState(true, $"{Msg.FellInPit}\n{Msg.LoseMessage}")
                : new EndState();
        }
    }
}