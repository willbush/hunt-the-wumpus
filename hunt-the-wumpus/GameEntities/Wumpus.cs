using System;

namespace HuntTheWumpus.GameEntities {
    public class Wumpus : DeadlyHazzard {
        private readonly Map _map;
        private bool IsAwake { get; set; }

        public Wumpus(Map map, int roomNumber) {
            _map = map;
            RoomNumber = roomNumber;
        }

        public override void Update(Player player) {
            if (!IsAwake && player.RoomNumber == RoomNumber) {
                Console.WriteLine(Msg.WumpusBump);
                IsAwake = true;
            }
            if (!IsAwake && player.CrookedArrowCount < player.MaxArrows)
                IsAwake = true;

            if (IsAwake)
                Move();
        }

        public void Move() {
            if (!WumpusFeelsLikeMoving()) return;

            RoomNumber = _map.GetSafeRoomNextTo(RoomNumber);
            Console.WriteLine($"Wumpus moved to {RoomNumber}");
        }

        private static bool WumpusFeelsLikeMoving() {
            return new Random().Next(1, 101) > 25; // 75% chance wumpus feels like moving.
        }

        public override void PrintLocation() {
            Console.WriteLine($"Wumpus in room {RoomNumber}");
        }

        public override void PrintHazardWarning() {
            Console.WriteLine(Msg.WumpusWarning);
        }

        public override EndState GetEndState(int playerRoomNumber) {
            if (IsAwake && playerRoomNumber == RoomNumber)
                return new EndState(true, $"{Msg.WumpusGotYou}\n{Msg.LoseMessage}");

            return new EndState();
        }
    }
}