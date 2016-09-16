using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HuntTheWumpus {
    public class Game {
        private const string LoseMessage = "Ha ha ha - you lose!";
        private const string WinMessage = "Aha! You got the Wumpus!\n" +
                                          "Hee hee hee - the Wumpus'll getcha next time!!";
        private readonly BottomlessPit _bottomlessPit1;
        private readonly BottomlessPit _bottomlessPit2;
        private readonly Player _player;
        private readonly HashSet<int> _roomsWithStaticHazards;
        private readonly SuperBats _superBats1;
        private readonly SuperBats _superBats2;
        private readonly Wumpus _wumpus;

        internal Game() {
            var occupiedRooms = new HashSet<int>();
            _player = new Player { RoomNumber = Map.GetRandomAvailableRoom(occupiedRooms) };
            _wumpus = new Wumpus { RoomNumber = Map.GetRandomAvailableRoom(occupiedRooms) };

            int superBatRoom1 = Map.GetRandomAvailableRoom(occupiedRooms);
            _superBats1 = new SuperBats { RoomNumber = superBatRoom1 };

            int superBatRoom2 = Map.GetRandomAvailableRoom(occupiedRooms);
            _superBats2 = new SuperBats { RoomNumber = superBatRoom2 };

            int bottomlessPitRoom1 = Map.GetRandomAvailableRoom(occupiedRooms);
            _bottomlessPit1 = new BottomlessPit { RoomNumber = bottomlessPitRoom1 };

            int bottomlessPitRoom2 = Map.GetRandomAvailableRoom(occupiedRooms);
            _bottomlessPit2 = new BottomlessPit { RoomNumber = bottomlessPitRoom2 };

            _roomsWithStaticHazards = new HashSet<int> {
                superBatRoom1,
                superBatRoom2,
                bottomlessPitRoom1,
                bottomlessPitRoom2
            };

            //TODO: remove lines below
            Console.WriteLine($"Superbats1 are in room number {_superBats1.RoomNumber}");
            Console.WriteLine($"Superbats2 are in room number {_superBats2.RoomNumber}");
            Console.WriteLine($"Wumpus is in room number {_wumpus.RoomNumber}");
            Console.WriteLine($"Bottomless pit1 is in room number {_bottomlessPit1.RoomNumber}");
            Console.WriteLine($"Bottomless pit2 is in room number {_bottomlessPit2.RoomNumber}");
        }

        public void Run() {
            const string playAgain = "Play again? (Y-N)";
            const string sameSetup = "Same Setup? (Y-N)";
            var acceptableResponses = new HashSet<string> { "Y", "N" };
            string playResponse;
            string setupResponse;
            do {
                Play();
                playResponse = GetValidResponse(playAgain, acceptableResponses);
                if (playResponse == "Y") {
                    setupResponse = GetValidResponse(sameSetup, acceptableResponses);
                    //TODO: reset game state.
                }
            } while (playResponse == "Y");
        }

        private void Play() {
            const string actionPrompt = "Shoot, Move or Quit(S - M - Q)? ";
            EndState endState;

            do {
                Console.WriteLine();
                UpdateWumpus();
                CheckIfPlayerMovedIntoRoomWithSuperbats();
                PrintAnyAdjacentHazards();
                Console.WriteLine($"You are in room {_player.RoomNumber}");
                Map.PrintAdjacentRoomNumbers(_player.RoomNumber);

                string command = GetValidResponse(actionPrompt, new HashSet<string> { "S", "M", "Q" });
                endState = GetEndState(command);
            } while (!endState.IsGameOver);

            endState.Print();
        }

        /// <summary>
        ///     Gets an acceptable response to a question.
        /// </summary>
        /// <param name="question">The question to ask</param>
        /// <param name="acceptableResponses">acceptable responses to the question (all assumed to be uppercase)</param>
        /// <returns>an acceptable response</returns>
        private static string GetValidResponse(string question, ICollection<string> acceptableResponses) {
            string response;
            do {
                Console.Write(question);
                response = Console.ReadLine()?.Trim().ToUpper();
            } while (!acceptableResponses.Contains(response));

            return response;
        }

        public static T Convert<T>(string input) {
            return (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
        }

        private void UpdateWumpus() {
            if (!_wumpus.IsAwake && _player.RoomNumber == _wumpus.RoomNumber) {
                Console.WriteLine("...Oops! Bumped a wumpus!");
                _wumpus.IsAwake = true;
            }
            if (!_wumpus.IsAwake && _player.CrookedArrowCount < _player.MaxArrows)
                _wumpus.IsAwake = true;

            if (_wumpus.IsAwake)
                _wumpus.Move(_roomsWithStaticHazards);
        }

        private void CheckIfPlayerMovedIntoRoomWithSuperbats() {
            if (_player.RoomNumber == _superBats1.RoomNumber || _player.RoomNumber == _superBats2.RoomNumber) {
                Console.WriteLine("Zap--Super Bat snatch! Elsewhereville for you!");
                _player.RoomNumber = Map.GetAnyRandomRoomNumber();
            }
        }

        private EndState GetEndState(string command) {
            EndState endState;
            switch (command) {
                case "M":
                    _player.Move();
                    endState = GetEndState();
                    break;
                case "S":
                    endState = GetEndState(_player.ShootArrow(), _player.RoomNumber, _wumpus.RoomNumber);
                    break;
                case "Q":
                    endState = new EndState(true, "");
                    break;
                default:
                    endState = new EndState(false, "");
                    break;
            }
            return endState;
        }

        private EndState GetEndState() {
            EndState endState;

            if (_player.RoomNumber == _bottomlessPit1.RoomNumber || _player.RoomNumber == _bottomlessPit2.RoomNumber)
                endState = new EndState(true, $"YYYIIIIEEEE... fell in a pit!\n{LoseMessage}");
            else if (_wumpus.IsAwake && _player.RoomNumber == _wumpus.RoomNumber)
                endState = new EndState(true, $"Tsk tsk tsk - wumpus got you!\n{LoseMessage}");
            else
                endState = new EndState(false, "");

            return endState;
        }

        private EndState GetEndState(IReadOnlyCollection<int> roomsToTraverse, int playerRoomNum, int wumpusRoomNum) {
            if (roomsToTraverse == null)
                return new EndState(false, "");

            int roomsTraversed = 0;
            var traversedRooms = new List<int> { playerRoomNum };
            int currentRoom = playerRoomNum;

            foreach (int nextRoom in roomsToTraverse) {
                HashSet<int> adjacentRooms = Map.Rooms[currentRoom];

                if (adjacentRooms.Contains(nextRoom)) {
                    traversedRooms.Add(currentRoom);
                    currentRoom = nextRoom;
                    Console.WriteLine(currentRoom);
                    roomsTraversed++;
                    if (playerRoomNum == currentRoom) {
                        return new EndState(true, $"Ouch! Arrow got you!\n{LoseMessage}");
                    }
                    if (wumpusRoomNum == currentRoom) {
                        return new EndState(true, WinMessage);
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
                if (playerRoomNum == currentRoom) {
                    return new EndState(true, $"Ouch! Arrow got you!\n{LoseMessage}");
                }
                if (wumpusRoomNum == currentRoom) {
                    return new EndState(true, WinMessage);
                }
            }
            Console.WriteLine("Missed!");
            if (_player.CrookedArrowCount == 0) {
                return new EndState(true, $"You've run out of arrows!\n{LoseMessage}");
            }
            return new EndState(false, "");
        }

        private void PrintAnyAdjacentHazards() {
            HashSet<int> adjacentRooms = Map.Rooms[_player.RoomNumber];

            if (adjacentRooms.Contains(_wumpus.RoomNumber))
                Console.WriteLine("I smell a Wumpus!");
            if (adjacentRooms.Contains(_superBats1.RoomNumber))
                Console.WriteLine("Bats nearby!");
            if (adjacentRooms.Contains(_superBats2.RoomNumber))
                Console.WriteLine("Bats nearby!");
            if (adjacentRooms.Contains(_bottomlessPit1.RoomNumber))
                Console.WriteLine("I feel a draft!");
            if (adjacentRooms.Contains(_bottomlessPit2.RoomNumber))
                Console.WriteLine("I feel a draft!");
        }
    }

    internal class EndState {
        public bool IsGameOver { get; }
        public string GameOverMessage { get; }

        internal EndState(bool isGameOver, string gameOverMessage) {
            IsGameOver = isGameOver;
            GameOverMessage = gameOverMessage;
        }

        public void Print() {
            Console.WriteLine();
            if (string.IsNullOrEmpty(GameOverMessage)) return;

            Console.WriteLine(GameOverMessage);
            Console.WriteLine();
        }
    }

    internal class GameEntity {
        public int RoomNumber { get; set; }
    }

    internal class Player : GameEntity {
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

        public List<int> ShootArrow() {
            int numOfRooms = GetNumRoomsToTraverse();
            if (numOfRooms == 0) {
                Console.WriteLine("OK, suit yourself...");
                return null;
            }
            CrookedArrowCount = CrookedArrowCount - 1;
            return GetRoomsToTraverse(numOfRooms);
        }

        private int GetNumRoomsToTraverse() {
            int numOfRooms;
            string response;
            do {
                Console.Write("No. or rooms (0-5)? ");
                response = Console.ReadLine();
            } while (!int.TryParse(response, out numOfRooms) || numOfRooms < 0 || numOfRooms > 5);

            return numOfRooms;
        }

        private static List<int> GetRoomsToTraverse(int numOfRooms) {
            var rooms = new List<int>();
            int count = 1;

            while (count <= numOfRooms) {
                Console.Write("Room #?");
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
}