using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus {
    internal class Game {
        private readonly BottomlessPit _bottomlessPit1;
        private readonly BottomlessPit _bottomlessPit2;
        private readonly Player _player;
        private readonly HashSet<int> _roomsWithStaticHazards;
        private readonly SuperBats _superBats;
        private readonly Wumpus _wumpus;

        internal Game() {
            var occupiedRooms = new HashSet<int>();
            _player = new Player { RoomNumber = Map.GetRandomAvailableRoom(occupiedRooms) };
            _wumpus = new Wumpus { RoomNumber = Map.GetRandomAvailableRoom(occupiedRooms) };

            int superBatRoom = Map.GetRandomAvailableRoom(occupiedRooms);
            _superBats = new SuperBats { RoomNumber = superBatRoom };

            int bottomlessPitRoom1 = Map.GetRandomAvailableRoom(occupiedRooms);
            _bottomlessPit1 = new BottomlessPit { RoomNumber = bottomlessPitRoom1 };

            int bottomlessPitRoom2 = Map.GetRandomAvailableRoom(occupiedRooms);
            _bottomlessPit2 = new BottomlessPit { RoomNumber = bottomlessPitRoom2 };

            _roomsWithStaticHazards = new HashSet<int> { superBatRoom, bottomlessPitRoom1, bottomlessPitRoom2 };

            //TODO: remove lines below
            Console.WriteLine($"Superbats are in room number {_superBats.RoomNumber}");
            Console.WriteLine($"Wumpus is in room number {_wumpus.RoomNumber}");
            Console.WriteLine($"Bottomless pit1 is in room number {_bottomlessPit1.RoomNumber}");
            Console.WriteLine($"Bottomless pit2 is in room number {_bottomlessPit2.RoomNumber}\n");
        }

        public void Run() {
            const string actionPrompt = "Shoot, Move or Quit(S - M - Q)? ";
            string command;
            do {
                UpdateWumpus();
                PrintAnyAdjacentHazards();
                Console.WriteLine($"You are in room {_player.RoomNumber}");
                Map.PrintAdjacentRoomNumbers(_player.RoomNumber);

                Console.Write(actionPrompt);
                command = Console.ReadLine();
                PerformCommand(command);
                Console.WriteLine();
            } while (!IsQuitCommand(command) && !IsGameOver());
        }

        private void UpdateWumpus() {
            if (!_wumpus.IsAwake && _player.RoomNumber == _wumpus.RoomNumber) {
                Console.WriteLine("...Oops! Bumped a wumpus!");
                _wumpus.IsAwake = true;
            }
            if (_wumpus.IsAwake)
                _wumpus.Move(_roomsWithStaticHazards);
        }

        private bool IsGameOver() {
            bool isGameOver = false;

            if (_player.RoomNumber == _bottomlessPit1.RoomNumber || _player.RoomNumber == _bottomlessPit2.RoomNumber) {
                Console.WriteLine("YYYIIIIEEEE... fell in a pit!");
                isGameOver = true;
            } else if (_wumpus.IsAwake && _player.RoomNumber == _wumpus.RoomNumber) {
                Console.WriteLine("Tsk tsk tsk - wumpus got you!");
                isGameOver = true;
            }
            if (isGameOver)
                Console.WriteLine("Ha ha ha - you lose!");

            return isGameOver;
        }

        private void PrintAnyAdjacentHazards() {
            HashSet<int> adjacentRooms = Map.Rooms[_player.RoomNumber];

            if (adjacentRooms.Contains(_wumpus.RoomNumber))
                Console.WriteLine("I smell a Wumpus!");
            if (adjacentRooms.Contains(_superBats.RoomNumber))
                Console.WriteLine("Bats nearby!");
            if (adjacentRooms.Contains(_bottomlessPit1.RoomNumber))
                Console.WriteLine("I feel a draft!");
            if (adjacentRooms.Contains(_bottomlessPit2.RoomNumber))
                Console.WriteLine("I feel a draft!");
        }

        private void PerformCommand(string cmd) {
            switch (cmd.Trim().ToUpper()) {
                case "M":
                    _player.Move();
                    break;
                case "S":
                    break;
            }
        }

        private static bool IsQuitCommand(string cmd) {
            return cmd.Trim().ToUpper() == "Q";
        }
    }

    internal class GameEntity {
        public int RoomNumber { get; set; }
    }

    internal class Player : GameEntity {
        public void Move() {
            Console.Write("Where to? ");
            string response = Console.ReadLine();

            int adjacentRoom;
            while (!int.TryParse(response, out adjacentRoom) || !Map.IsAdjacent(RoomNumber, adjacentRoom)) {
                Console.Write("Not Possible - Where to? ");
                response = Console.ReadLine();
            }
            RoomNumber = adjacentRoom;
        }
    }

    internal class Wumpus : GameEntity {
        public bool IsAwake { get; set; }

        public void Move(HashSet<int> roomsWithStaticHazards) {
            if (!WumpusFeelsLikeMoving()) return;

            int[] safeAdjacentRooms = Map.Rooms[RoomNumber].Except(roomsWithStaticHazards).ToArray();
            RoomNumber = safeAdjacentRooms.ElementAt(new Random().Next(safeAdjacentRooms.Length));
            Console.WriteLine($"Wumpus moved to {RoomNumber}");
        }

        private static bool WumpusFeelsLikeMoving() {
            return new Random().Next(1, 101) > 25; // 75% chance wumpus feels like moving.
        }
    }

    internal class SuperBats : GameEntity {}

    internal class BottomlessPit : GameEntity {}

    public static class Map {
        private static readonly Random Random = new Random();
        // Each key is the room number and its value is the set of adjacent rooms.

        internal static Dictionary<int, HashSet<int>> Rooms { get; } = new Dictionary<int, HashSet<int>> {
            { 1, new HashSet<int> { 2, 5, 8 } },
            { 2, new HashSet<int> { 1, 3, 10 } },
            { 3, new HashSet<int> { 2, 4, 12 } },
            { 4, new HashSet<int> { 3, 5, 14 } },
            { 5, new HashSet<int> { 1, 4, 6 } },
            { 6, new HashSet<int> { 5, 7, 15 } },
            { 7, new HashSet<int> { 6, 8, 17 } },
            { 8, new HashSet<int> { 1, 7, 9 } },
            { 9, new HashSet<int> { 8, 10, 18 } },
            { 10, new HashSet<int> { 2, 9, 11 } },
            { 11, new HashSet<int> { 10, 12, 19 } },
            { 12, new HashSet<int> { 3, 11, 13 } },
            { 13, new HashSet<int> { 12, 14, 20 } },
            { 14, new HashSet<int> { 4, 13, 15 } },
            { 15, new HashSet<int> { 6, 14, 16 } },
            { 16, new HashSet<int> { 15, 17, 20 } },
            { 17, new HashSet<int> { 7, 16, 18 } },
            { 18, new HashSet<int> { 9, 17, 19 } },
            { 19, new HashSet<int> { 11, 18, 20 } },
            { 20, new HashSet<int> { 13, 16, 19 } }
        };

        public static void PrintAdjacentRoomNumbers(int roomNum) {
            var sb = new StringBuilder();
            foreach (int room in Rooms[roomNum])
                sb.Append(room + " ");

            Console.WriteLine($"Tunnels lead to {sb}");
        }

        public static bool IsAdjacent(int currentRoom, int adjacentRoom) {
            return Rooms[currentRoom].Contains(adjacentRoom);
        }

        public static int GetRandomAvailableRoom(HashSet<int> occupiedRooms) {
            const int numOfRooms = 20;
            int[] availableRooms = Enumerable.Range(1, numOfRooms).Where(r => !occupiedRooms.Contains(r)).ToArray();
            if (availableRooms.Length == 0)
                throw new InvalidOperationException("All rooms are already occupied.");

            int index = Random.Next(0, availableRooms.Length);
            int unoccupiedRoom = availableRooms[index];
            occupiedRooms.Add(unoccupiedRoom);
            return unoccupiedRoom;
        }
    }
}