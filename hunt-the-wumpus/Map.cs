using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus {
    public class Map {
        public const int NumOfRooms = 20;
        private static readonly Random Random = new Random();
        private readonly int _playerInitalRoomNumber;
        private readonly int _wumpusInitalRoomNumber;
        private readonly List<Hazzard> _hazzards;
        private readonly List<DeadlyHazzard> _deadlyHazzards;
        private readonly HashSet<int> _roomsWithStaticHazards;
        public Player Player { get; private set; }
        public Wumpus Wumpus { get; private set; }

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

        public Map() {
            var occupiedRooms = new HashSet<int>();

            _playerInitalRoomNumber = GetRandomAvailableRoom(occupiedRooms);
            Player = new Player { RoomNumber = _playerInitalRoomNumber };
            _wumpusInitalRoomNumber = GetRandomAvailableRoom(occupiedRooms);
            Wumpus = new Wumpus(this, _wumpusInitalRoomNumber);

            _hazzards = new List<Hazzard> { Wumpus };
            _deadlyHazzards = new List<DeadlyHazzard> { Wumpus };

            int superBatRoom1 = GetRandomAvailableRoom(occupiedRooms);
            _hazzards.Add(new SuperBats(superBatRoom1));

            int superBatRoom2 = GetRandomAvailableRoom(occupiedRooms);
            _hazzards.Add(new SuperBats(superBatRoom2));

            int bottomlessPitRoom1 = GetRandomAvailableRoom(occupiedRooms);
            _hazzards.Add(new BottomlessPit { RoomNumber = bottomlessPitRoom1 });
            _deadlyHazzards.Add(new BottomlessPit { RoomNumber = bottomlessPitRoom1 });

            int bottomlessPitRoom2 = GetRandomAvailableRoom(occupiedRooms);
            _hazzards.Add(new BottomlessPit { RoomNumber = bottomlessPitRoom2 });
            _deadlyHazzards.Add(new BottomlessPit { RoomNumber = bottomlessPitRoom2 });

            _roomsWithStaticHazards = new HashSet<int> {
                superBatRoom1,
                superBatRoom2,
                bottomlessPitRoom1,
                bottomlessPitRoom2
            };

            //TODO: remove lines below
            _hazzards.ForEach(h => h.PrintLocation());
        }

        public void Reset() {
            Player = new Player { RoomNumber = _playerInitalRoomNumber };
            Wumpus = new Wumpus(this, _wumpusInitalRoomNumber);

            //TODO: remove lines below
            _hazzards.ForEach(h => h.PrintLocation());
        }

        public int GetRandomAvailableRoom(HashSet<int> occupiedRooms) {
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

        public void Update() {
            Console.WriteLine();
            HashSet<int> roomsAdjacentToPlayer = Rooms[Player.RoomNumber];
            _hazzards.ForEach(
                h => {
                    h.Update(Player);
                    if (roomsAdjacentToPlayer.Contains(h.RoomNumber))
                        h.PrintHazardWarning();
                });
            Player.PrintLocation();
        }

        public int GetSafeRoomNextTo(int roomNumber) {
            int[] safeAdjacentRooms = Rooms[roomNumber].Except(_roomsWithStaticHazards).ToArray();
            return safeAdjacentRooms.ElementAt(new Random().Next(safeAdjacentRooms.Length));
        }

        public EndState GetEndState(string command) {
            EndState endState;
            switch (command) {
                case "M":
                    Player.Move();
                    endState = GetEndState();
                    break;
                case "S":
                    endState = Player.ShootArrow();
                    break;
                case "Q":
                    endState = new EndState(true, "");
                    break;
                default:
                    endState = new EndState();
                    break;
            }
            return endState;
        }

        private EndState GetEndState() {
            EndState endState =
                _deadlyHazzards.Select(h => h.GetEndState(Player.RoomNumber)).FirstOrDefault(s => s.IsGameOver);
            return endState ?? new EndState();
        }

        public static void PrintAdjacentRoomNumbers(int roomNum) {
            var sb = new StringBuilder();
            foreach (int room in Rooms[roomNum])
                sb.Append(room + " ");

            Console.WriteLine($"Tunnels lead to {sb}");
        }

        public static bool IsAdjacent(int currentRoom, int adjacentRoom) {
            return Rooms[currentRoom].Contains(adjacentRoom);
        }
    }

    public abstract class GameEntity {
        public int RoomNumber { get; set; }
        public abstract void PrintLocation();
    }

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

        public EndState ShootArrow() {
            return null;
        }

        public List<int> Temp() {
            int numOfRooms = GetNumRoomsToTraverse();
            if (numOfRooms == 0) {
                Console.WriteLine("OK, suit yourself...");
                return null;
            }
            CrookedArrowCount = CrookedArrowCount - 1;
            return GetRoomsToTraverse(numOfRooms);
        }

        private static int GetNumRoomsToTraverse() {
            int numOfRooms;
            string response;
            do {
                Console.Write(Msg.RoomNumPrompt);
                response = Console.ReadLine();
            } while (!int.TryParse(response, out numOfRooms) || numOfRooms < 0 || numOfRooms > 5);

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

    public abstract class Hazzard : GameEntity {
        public abstract void PrintHazardWarning();
        public abstract void Update(Player player);
    }

    public abstract class DeadlyHazzard : Hazzard {
        public abstract EndState GetEndState(int playerRoomNumber);
    }

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

    public class BottomlessPit : DeadlyHazzard {
        public override void PrintLocation() {
            Console.WriteLine($"Bottomless pit in room {RoomNumber}");
        }

        public override void PrintHazardWarning() {
            Console.WriteLine(Msg.PitWarning);
        }

        public override void Update(Player player) {}

        public override EndState GetEndState(int playerRoomNumber) {
            return playerRoomNumber == RoomNumber
                ? new EndState(true, $"{Msg.FellInPit}\n{Msg.LoseMessage}")
                : new EndState();
        }
    }
}