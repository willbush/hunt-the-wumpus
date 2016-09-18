using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.GameEntities {
    public class Player : GameEntity {
        private const int MaxNumberOfArrows = 5;
        public int MaxArrows { get; } = MaxNumberOfArrows;
        public int CrookedArrowCount { get; private set; } = MaxNumberOfArrows;

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

        public EndState ShootArrow(int wumpusRoomNumber) {
            EndState endState;
            int numOfRooms = GetNumRoomsToTraverse();

            if (numOfRooms > 0) {
                endState = ShootArrow(GetRoomsToTraverse(numOfRooms), wumpusRoomNumber);
                CrookedArrowCount = CrookedArrowCount - 1;
            } else {
                Console.WriteLine("OK, suit yourself...");
                endState = new EndState();
            }
            return endState;
        }

        private EndState ShootArrow(IReadOnlyCollection<int> roomsToTraverse, int wumpusRoomNum) {
            EndState endstate = Traverse(roomsToTraverse).Select(r => HitTarget(r, wumpusRoomNum))
                .FirstOrDefault(e => e.IsGameOver);

            if (endstate != null) return endstate;

            Console.WriteLine(Msg.Missed);
            return CrookedArrowCount == 0
                ? new EndState(true, $"{Msg.OutOfArrows}\n{Msg.LoseMessage}")
                : new EndState();
        }

        private EndState HitTarget(int currentRoom, int wumpusRoomNum) {
            Console.WriteLine(currentRoom);
            EndState endState;
            if (RoomNumber == currentRoom) {
                endState = new EndState(true, $"{Msg.ArrowGotYou}\n{Msg.LoseMessage}");
            } else if (wumpusRoomNum == currentRoom) {
                endState = new EndState(true, Msg.WinMessage);
            } else {
                endState = new EndState();
            }
            return endState;
        }

        private IEnumerable<int> Traverse(IReadOnlyCollection<int> roomsToTraverse) {
            int currentRoom = RoomNumber;

            ICollection<int> traversedRooms = roomsToTraverse.TakeWhile(
                nextRoom => {
                    HashSet<int> adjacentRooms = Map.Rooms[currentRoom];
                    if (!adjacentRooms.Contains(nextRoom)) return false;
                    currentRoom = nextRoom;
                    return true;
                }).ToList();

            int numLeftToTraverse = roomsToTraverse.Count - traversedRooms.Count;
            RandomlyTraverse(traversedRooms, currentRoom, numLeftToTraverse);
            return traversedRooms;
        }

        private static void RandomlyTraverse(
            ICollection<int> traversedRooms,
            int currentRoom,
            int numberToTraverse) {
            int previousRoom;

            if (!traversedRooms.Any()) {
                HashSet<int> rooms = Map.Rooms[currentRoom];
                int firstRoom = rooms.ElementAt(new Random().Next(rooms.Count));

                previousRoom = currentRoom;
                currentRoom = firstRoom;
            } else {
                previousRoom = currentRoom;
            }

            for (int traversed = 0; traversed < numberToTraverse; ++traversed) {
                int[] rooms = Map.Rooms[currentRoom].Where(r => r != previousRoom).ToArray();
                int nextRoom = rooms.ElementAt(new Random().Next(rooms.Length));

                traversedRooms.Add(currentRoom);
                previousRoom = currentRoom;
                currentRoom = nextRoom;
            }
        }

        private static int GetNumRoomsToTraverse() {
            const int lowerBound = 0;
            const int upperBound = 5;
            int numOfRooms;
            string response;

            do {
                Console.Write(Msg.NumOfRoomsToShootPrompt);
                response = Console.ReadLine();
            } while (!int.TryParse(response, out numOfRooms) || numOfRooms < lowerBound || numOfRooms > upperBound);

            return numOfRooms;
        }

        private static List<int> GetRoomsToTraverse(int numOfRooms) {
            var rooms = new List<int>();
            int count = 1;

            while (count <= numOfRooms) {
                Console.Write(Msg.RoomNumPrompt);
                int roomNumber;
                if (!int.TryParse(Console.ReadLine(), out roomNumber) || roomNumber < 0 || roomNumber > Map.NumOfRooms) {
                    Console.WriteLine("Bad number - try again:");
                    continue;
                }
                if (IsTooCrooked(roomNumber, rooms)) {
                    Console.WriteLine(Msg.TooCrooked);
                } else {
                    rooms.Add(roomNumber);
                    count = count + 1;
                }
            }
            return rooms;
        }

        private static bool IsTooCrooked(int roomNumber, IReadOnlyList<int> rooms) {
            return (rooms.Count > 0 && rooms.Last() == roomNumber) ||
                   (rooms.Count > 1 && rooms[rooms.Count - 2] == roomNumber);
        }

        public override void PrintLocation() {
            Console.WriteLine($"You are in room {RoomNumber}");
            Map.PrintAdjacentRoomNumbers(RoomNumber);
        }
    }
}