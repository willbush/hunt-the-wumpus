using System;

namespace HuntTheWumpus.GameEntities {
    public class Wumpus : DeadlyHazard {
        private readonly Map _map;
        private bool IsAwake { get; set; }

        public Wumpus(Map map, int roomNumber) {
            _map = map;
            RoomNumber = roomNumber;
        }

        /// <summary>
        ///     Updates the state of the wumpus.
        /// </summary>
        /// <param name="player">the player</param>
        public override void Update(Player player) {
            if (!IsAwake && player.RoomNumber == RoomNumber) {
                Console.WriteLine(Message.WumpusBump);
                IsAwake = true;
            }
            if (!IsAwake && player.CrookedArrowCount < player.MaxArrows)
                IsAwake = true;

            if (IsAwake)
                Move();
        }

        /// <summary>
        ///     Moves the wumpus with a 75% chance.
        /// </summary>
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
            Console.WriteLine(Message.WumpusWarning);
        }

        /// <summary>
        ///     Determine the game end state given the player's current room number.
        /// </summary>
        /// <param name="playerRoomNumber">current player room number</param>
        /// <returns>end state</returns>
        public override EndState DetermineEndState(int playerRoomNumber) {
            if (IsAwake && playerRoomNumber == RoomNumber)
                return new EndState(true, $"{Message.WumpusGotYou}\n{Message.LoseMessage}");

            return new EndState();
        }
    }
}