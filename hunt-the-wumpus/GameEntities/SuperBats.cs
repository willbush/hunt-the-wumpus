using System;

namespace HuntTheWumpus.GameEntities {
    public class SuperBats : Hazard {
        public SuperBats(int roomNumber) : base(roomNumber) {}

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
        /// <param name="player"></param>
        /// <returns>true if the bat snatched the player into another room</returns>
        public bool TrySnatch(Player player) {
            if (player.RoomNumber != RoomNumber) return false;

            Console.WriteLine(Message.BatSnatch);
            player.Move(Map.GetAnyRandomRoomNumber());
            return true;
        }
    }
}