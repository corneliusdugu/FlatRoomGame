using System;
using System.Collections.Generic;
using System.Linq;
using FlatRoomGame.Characters;
using FlatRoomGame.Challenges;
using FlatRoomGame.Commands;
using FlatRoomGame.Rooms;
using FlatRoomGame.Factories;

namespace FlatRoomGame.Core
{
    // main game controller
    public class GameEngine
    {
        public Player Player { get; private set; } = null!; // player ref
        public IRoom CurrentRoom { get; set; } = null!; // current room
        private readonly List<IRoom> _allRooms = new(); // all rooms in game

        public void Start()
        {
            Console.Clear(); // clear screen
            Console.WriteLine("🎮 Welcome to the Flat Room Adventure Game!"); // show intro
            SetupGame(); // setup everything
            RunGameLoop(); // start main loop
        }

        // make player and rooms
        private void SetupGame()
        {
            Console.Write("👤 Enter your name: ");
            string playerName = Console.ReadLine() ?? "Player"; // get player name

            Console.Write("⚧ Enter your gender: ");
            string gender = Console.ReadLine() ?? "Unknown"; // get gender

            // traits to pick from
            List<string> traits = new List<string>
            {
                "Brave", "Clever", "Strong", "Curious", "Bold", "Calm", "Swift", "Wise"
            };

            Console.WriteLine("\n🎯 Choose the trait of the person you're searching for:");
            for (int i = 0; i < traits.Count; i++) // show all traits
            {
                Console.WriteLine($"{i + 1}. {traits[i]}");
            }

            Console.Write("Enter the number of your choice: ");
            int choice = -1;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > traits.Count) // validate input
            {
                Console.Write("❌ Invalid choice. Please enter a number between 1 and {0}: ", traits.Count);
            }

            string selectedTrait = traits[choice - 1]; // get selected trait
            Console.WriteLine($"\n🕵️ You are searching for someone who is: {selectedTrait}\n");

            Player = new Player(playerName, gender, selectedTrait); // make player

            var factory = new FlatMysteryRoomFactory(); // make room factory
            _allRooms.Clear(); // clear room list

            // helper to get random other trait
            string GetRandomTrait()
            {
                var otherTraits = traits.Where(t => t != selectedTrait).ToList();
                var rand = new Random();
                return otherTraits[rand.Next(otherTraits.Count)];
            }

            // make all rooms
            _allRooms.Add(factory.CreateSkillRoom("Room 105A", GetRandomTrait()));
            _allRooms.Add(factory.CreatePhysicalRoom("Room 105B", GetRandomTrait()));
            _allRooms.Add(factory.CreateSkillRoom("Room 105C", GetRandomTrait()));
            _allRooms.Add(factory.CreatePhysicalRoom("Room 105D", GetRandomTrait()));
            _allRooms.Add(factory.CreateUltimateRoom("Room 105E", selectedTrait));

            _allRooms.Sort((a, b) => Guid.NewGuid().CompareTo(Guid.NewGuid())); // shuffle rooms
            WireGridMap(_allRooms, columns: 3); // connect rooms

            CurrentRoom = _allRooms[0]; // start in first room
            CurrentRoom.Enter(Player); // enter first room
        }

        // connect rooms like a grid
        private void WireGridMap(IList<IRoom> rooms, int columns)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i] is BaseRoom current)
                {
                    int col = i % columns;

                    // east room
                    if (col + 1 < columns && i + 1 < rooms.Count)
                    {
                        var east = rooms[i + 1];
                        current.Connect("east", east);
                        if (east is BaseRoom eastBase) eastBase.Connect("west", current);
                    }

                    // south room
                    int southIndex = i + columns;
                    if (southIndex < rooms.Count)
                    {
                        var south = rooms[southIndex];
                        current.Connect("south", south);
                        if (south is BaseRoom southBase) southBase.Connect("north", current);
                    }
                }
            }
        }

        // main game loop
        private void RunGameLoop()
        {
            while (Player.IsAlive()) // run till player dies
            {
                var room = CurrentRoom;
                Console.WriteLine($"\n➡️ You enter {room.RoomName} ({room.RoomType})");
                Console.WriteLine("❓ A challenge awaits you...");

                var invoker = new CommandInvoker(); // command system
                invoker.RegisterCommand(new StatusCommand(Player));
                invoker.RegisterCommand(new HelpCommand(invoker));
                invoker.RegisterCommand(new MapCommand(this));

                bool puzzleDone = false;
                bool combatDone = false;

                // add room challenges
                if (room is PhysicalRoom)
                {
                    var combat = new CombatChallenge("Enemy", 40);
                    invoker.RegisterCommand(
                        new AttackCommand(Player, combat, success =>
                        {
                            combatDone = success;
                            if (success && room is BaseRoom br) br.SetCleared();
                        })
                    );
                }
                else if (room is SkillRoom)
                {
                    var puzzle = new MathPuzzle();
                    invoker.RegisterCommand(
                        new SolveCommand(Player, puzzle, success =>
                        {
                            puzzleDone = success;
                            if (success && room is BaseRoom br) br.SetCleared();
                        })
                    );
                }
                else if (room is UltimateRoom)
                {
                    var combat = new CombatChallenge("Master", 70);
                    var puzzle = new MathPuzzle();

                    invoker.RegisterCommand(
                        new AttackCommand(Player, combat, success =>
                        {
                            combatDone = success;
                            if (success && puzzleDone && room is BaseRoom br) br.SetCleared();
                        })
                    );

                    invoker.RegisterCommand(
                        new SolveCommand(Player, puzzle, success =>
                        {
                            puzzleDone = success;
                            if (success && combatDone && room is BaseRoom br) br.SetCleared();
                        })
                    );
                }

                // challenge loop
                while (Player.IsAlive() && !room.IsCleared)
                {
                    Console.Write("\n💡 Enter a command (attack, solve, help, status): ");
                    string? input = Console.ReadLine()?.Trim().ToLower(); // get input
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("⚠️ Invalid command.");
                        continue;
                    }

                    invoker.ExecuteCommand(input); // run command

                    // if player died
                    if (!Player.IsAlive())
                    {
                        Console.WriteLine("\n☠️ You were defeated. Game over!");
                        Console.Write("\nWould you like to play again? (y/n): ");
                        string? restart = Console.ReadLine()?.Trim().ToLower();

                        if (restart == "y" || restart == "yes")
                        {
                            Console.Clear();
                            Start(); // restart
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\n👋 Thanks for playing. Goodbye!");
                            Environment.Exit(0);
                        }
                    }
                }

                // safety check if dead
                if (!Player.IsAlive())
                {
                    Console.WriteLine("\n☠️ You were defeated. Game over!");
                    Console.Write("\nWould you like to play again? (y/n): ");
                    string? restart = Console.ReadLine()?.Trim().ToLower();

                    if (restart == "y" || restart == "yes")
                    {
                        Console.Clear();
                        Start();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\n👋 Thanks for playing. Goodbye!");
                        Environment.Exit(0);
                    }
                }

                // show cleared room info
                Console.WriteLine($"\n✨ You have cleared {room.RoomName}!");
                Console.WriteLine($"🔑 Trait revealed: {room.Trait}");

                // check win
                if (room.Trait == Player.Trait)
                {
                    Console.WriteLine("\n🏆 You found the person with the matching trait! You win!");
                    return;
                }

                // room summary
                Console.WriteLine("\n📜 Room Summary:");
                Console.WriteLine($"- Cleared Room: {room.RoomName}");
                Console.WriteLine($"- Trait Revealed: {room.Trait}");
                Console.WriteLine($"- Health Remaining: {Player.Health}");

                Console.WriteLine("\n(Press Enter to continue...)");
                Console.ReadLine();

                // movement setup
                var moveInvoker = new CommandInvoker();
                moveInvoker.RegisterCommand(new DirectionalMoveCommand(this, "north", "north", "Move north."));
                moveInvoker.RegisterCommand(new DirectionalMoveCommand(this, "south", "south", "Move south."));
                moveInvoker.RegisterCommand(new DirectionalMoveCommand(this, "east", "east", "Move east."));
                moveInvoker.RegisterCommand(new DirectionalMoveCommand(this, "west", "west", "Move west."));

                // synonyms for directions
                moveInvoker.RegisterCommand(new DirectionalMoveCommand(this, "forward", "north", "Move forward (north)."));
                moveInvoker.RegisterCommand(new DirectionalMoveCommand(this, "back", "south", "Move back (south)."));
                moveInvoker.RegisterCommand(new DirectionalMoveCommand(this, "left", "west", "Move left (west)."));
                moveInvoker.RegisterCommand(new DirectionalMoveCommand(this, "right", "east", "Move right (east)."));

                moveInvoker.RegisterCommand(new MapCommand(this));

                Console.Clear(); // clear screen after room done

                Console.WriteLine("\n🧭 Choose a direction (north/south/east/west) or type 'map':");

                IRoom beforeMove = CurrentRoom; // keep track of current
                while (Player.IsAlive() && ReferenceEquals(beforeMove, CurrentRoom))
                {
                    string? input = Console.ReadLine()?.Trim().ToLower(); // get move input
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("⚠️ Enter a direction or 'map'.");
                        continue;
                    }

                    moveInvoker.ExecuteCommand(input); // run move command

                    if (ReferenceEquals(beforeMove, CurrentRoom))
                        Console.WriteLine("⏭️ Try another direction or 'map'."); // invalid move
                }

                Console.Clear(); // clear after move
            }

            Console.WriteLine("\n🏁 Game over. Thanks for playing!"); // end
        }
    }
}
