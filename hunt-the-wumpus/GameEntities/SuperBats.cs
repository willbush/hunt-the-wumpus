using System;

namespace HuntTheWumpus.GameEntities {
    public class SuperBats : Hazzard {
        public SuperBats(int roomNumber) {
            RoomNumber = roomNumber;
        }

        public override void PrintLocation() {
            Console.WriteLine($"SuperBats in room {RoomNumber}");
        }

        public override void PrintHazardWarning() {
            Console.WriteLine(Msg.BatWarning);
        }

        public override void Update(Player player) {
            if (player.RoomNumber != RoomNumber) return;

            Console.WriteLine(Msg.BatSnatch);
            player.RoomNumber = Map.GetAnyRandomRoomNumber();
        }
    }
}