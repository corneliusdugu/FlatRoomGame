using System;
using FlatRoomGame.Core;
using FlatRoomGame.Rooms;

namespace FlatRoomGame.Commands
{
    // command for moving to another room in a direction
    public class DirectionalMoveCommand : ICommand
    {
        private readonly GameEngine _engine; // game controller
        private readonly string _wireDirection; // real direction key
        private readonly string _name; // command name
        private readonly string _desc; // command description

        public DirectionalMoveCommand(GameEngine engine, string name, string wireDirection, string description)
        {
            _engine = engine; // set engine
            _name = name.ToLower(); // set name
            _wireDirection = wireDirection.ToLower(); // set direction
            _desc = description; // set description
        }

        public string Name => _name; // get name
        public string Description => _desc; // get description

        public void Execute()
        {
            IRoom current = _engine.CurrentRoom; // get current room
            if (!current.IsCleared) // if not cleared
            {
                Console.WriteLine("⛔ You cannot leave this room until all challenges are cleared."); // warn player
                return;
            }

            if (current.Exits.TryGetValue(_wireDirection, out var next)) // check exit
            {
                _engine.CurrentRoom = next; // move to next room
                Console.WriteLine($"🧭 You move {_name} to {next.RoomName} ({next.RoomType})."); // show move info
                next.Enter(_engine.Player); // enter new room
            }
            else
            {
                Console.WriteLine($"🚫 There is no exit {_name} from here."); // no exit that way
            }
        }
    }
}
