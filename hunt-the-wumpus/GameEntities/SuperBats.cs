using System;

namespace HuntTheWumpus.GameEntities {
    public class SuperBats : Hazard {
        public SuperBats(int roomNumber) {
            RoomNumber = roomNumber;
        }

        public override void PrintLocation() {
            Console.WriteLine($"SuperBats in room {RoomNumber}");
        }

        public override void PrintHazardWarning() {
            Console.WriteLine(Message.BatWarning);
        }

        /// <summary>
        ///     Moves player to a random location on the map if they enter the
        ///     same room as a super bat.
        /// </summary>
        /// <param name="player">the player</param>
        public override void Update(Player player) {
            if (player.RoomNumber != RoomNumber) return;

            Console.WriteLine(Message.BatSnatch);
            player.RoomNumber = Map.GetAnyRandomRoomNumber();
        }
    }
}