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
                endState = GetEndState(GetRoomsToTraverse(numOfRooms), wumpusRoomNumber);
                CrookedArrowCount = CrookedArrowCount - 1;
            } else {
                Console.WriteLine("OK, suit yourself...");
                endState = new EndState();
            }
            return endState;
        }

        private EndState GetEndState(IReadOnlyCollection<int> roomsToTraverse, int wumpusRoomNum) {
            if (roomsToTraverse == null)
                return new EndState();

            int roomsTraversed = 0;
            var traversedRooms = new List<int> { RoomNumber };
            int currentRoom = RoomNumber;

            foreach (int nextRoom in roomsToTraverse) {
                HashSet<int> adjacentRooms = Map.Rooms[currentRoom];

                if (adjacentRooms.Contains(nextRoom)) {
                    traversedRooms.Add(currentRoom);
                    currentRoom = nextRoom;
                    Console.WriteLine(currentRoom);
                    roomsTraversed++;
                    if (RoomNumber == currentRoom) {
                        return new EndState(true, $"Ouch! Arrow got you!\n{Msg.LoseMessage}");
                    }
                    if (wumpusRoomNum == currentRoom) {
                        return new EndState(true, Msg.WinMessage);
                    }
                } else {
                    break;
                }
            }
            while (roomsTraversed < roomsToTraverse.Count) {
                int[] rooms = Map.Rooms[currentRoom].Where(r => r != traversedRooms.Last()).ToArray();
                int nextRoom = rooms.ElementAt(new Random().Next(rooms.Length));

                traversedRooms.Add(currentRoom);
                currentRoom = nextRoom;
                Console.WriteLine(currentRoom);
                roomsTraversed++;
                if (RoomNumber == currentRoom) {
                    return new EndState(true, $"Ouch! Arrow got you!\n{Msg.LoseMessage}");
                }
                if (wumpusRoomNum == currentRoom) {
                    return new EndState(true, Msg.WinMessage);
                }
            }
            Console.WriteLine("Missed!");
            if (CrookedArrowCount == 0) {
                return new EndState(true, $"You've run out of arrows!\n{Msg.LoseMessage}");
            }
            return new EndState();
        }

        private static int GetNumRoomsToTraverse() {
            const int lowerBound = 0;
            const int upperBound = 5;
            int numOfRooms;
            string response;

            do {
                Console.Write(Msg.RoomNumPrompt);
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
                    Console.WriteLine("Arrows aren't that crooked - try another room!");
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