using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus {
    public static class Map {
        public const int NumOfRooms = 20;
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
            int[] availableRooms = Enumerable.Range(1, NumOfRooms).Where(r => !occupiedRooms.Contains(r)).ToArray();
            if (availableRooms.Length == 0)
                throw new InvalidOperationException("All rooms are already occupied.");

            int index = Random.Next(0, availableRooms.Length);
            int unoccupiedRoom = availableRooms[index];
            occupiedRooms.Add(unoccupiedRoom);
            return unoccupiedRoom;
        }

        public static int GetAnyRandomRoomNumber() {
            return Random.Next(1, NumOfRooms + 1); // random number in range [1, 20]
        }
    }
}