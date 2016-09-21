using System;

namespace HuntTheWumpus.GameEntities {
    public class BottomlessPit : DeadlyHazard {
        public BottomlessPit(int roomNumber) : base(roomNumber) {}

        public override void PrintLocation() {
            Console.WriteLine($"Bottomless pit in room {RoomNumber}");
        }

        public override void PrintHazardWarning() {
            Console.WriteLine(Message.PitWarning);
        }

        public override EndState DetermineEndState(int playerRoomNumber) {
            return playerRoomNumber == RoomNumber
                ? new EndState(true, $"{Message.FellInPit}\n{Message.LoseMessage}")
                : new EndState();
        }
    }
}