using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus {
    internal class Game {
        private readonly DodecahedronMap _map;
        private readonly Player _player;

        internal Game() {
            _map = new DodecahedronMap();
            _player = new Player { RoomNumber = _map.GetRandomAvailableRoom() };
        }

        public void Run() {
            const string actionPrompt = "Shoot, Move or Quit(S - M - Q)? ";
            string respose;
            do {
                Console.WriteLine($"You are in room {_player.RoomNumber}");
                _map.PrintAdjacentRoomNumbers(_player.RoomNumber);

                Console.Write(actionPrompt);
                respose = Console.ReadLine();
                PerformCommand(respose);
            } while (!IsQuitCommand(respose));
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

    internal class Player {
        public int RoomNumber { get; set; }

        public void Move() {
            Console.Write("Where to? ");
            string response = Console.ReadLine();

            int adjacentRoom;
            while (!int.TryParse(response, out adjacentRoom) || !DodecahedronMap.IsAdjacent(RoomNumber, adjacentRoom)) {
                Console.Write("Not Possible - Where to? ");
                response = Console.ReadLine();
            }
            RoomNumber = adjacentRoom;
        }
    }

    public class DodecahedronMap {
        // Index + 1 is the room number. Each row are adjacent room numbers for the colomn room number;
        private static readonly int[,] Rooms = {
            { 2, 5, 8 },
            { 1, 3, 10 },
            { 2, 4, 12 },
            { 3, 5, 14 },
            { 1, 4, 6 },
            { 5, 7, 15 },
            { 6, 8, 17 },
            { 1, 7, 9 },
            { 8, 10, 18 },
            { 2, 9, 11 },
            { 10, 12, 19 },
            { 3, 11, 13 },
            { 12, 14, 20 },
            { 4, 13, 15 },
            { 6, 14, 16 },
            { 15, 17, 20 },
            { 7, 16, 18 },
            { 9, 17, 19 },
            { 11, 18, 20 },
            { 13, 16, 19 }
        };
        private readonly HashSet<int> _occupiedRooms = new HashSet<int>();
        private readonly Random _random = new Random();

        public void PrintAdjacentRoomNumbers(int roomNum) {
            int i = roomNum - 1;
            Console.WriteLine($"Tunnels lead to {Rooms[i, 0]} {Rooms[i, 1]} {Rooms[i, 2]}");
        }

        public static bool IsAdjacent(int currentRoom, int adjacentRoom) {
            int i = currentRoom - 1;
            for (int j = 0; j < Rooms.GetLength(1); ++j)
                if (Rooms[i, j] == adjacentRoom)
                    return true;

            return false;
        }

        public int GetRandomAvailableRoom() {
            int[] availableRooms = Enumerable.Range(1, 20).Where(r => !_occupiedRooms.Contains(r)).ToArray();
            if (availableRooms.Length == 0)
                throw new InvalidOperationException("All rooms are already occupied.");

            int index = _random.Next(0, availableRooms.Length);
            int unoccupiedRoom = availableRooms[index];
            _occupiedRooms.Add(unoccupiedRoom);
            return unoccupiedRoom;
        }
    }
}