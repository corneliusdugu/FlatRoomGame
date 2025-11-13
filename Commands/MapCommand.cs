using System;
using FlatRoomGame.Core;
using FlatRoomGame.Rooms;

namespace FlatRoomGame.Commands
{
    // command to show exits on map
    public class MapCommand : ICommand
    {
        private readonly GameEngine _engine; // game engine ref

        public MapCommand(GameEngine engine) => _engine = engine; // set engine

        public string Name => "map"; // command name
        public string Description => "Show available exits from the current room."; // command info

        public void Execute()
        {
            IRoom room = _engine.CurrentRoom; // get current room
            if (room.Exits.Count == 0) // if no exits
            {
                Console.WriteLine("🗺️ No visible exits."); // say no exits
                return;
            }

            Console.WriteLine("🗺️ Exits:"); // show exits
            foreach (var kv in room.Exits) // loop exits
            {
                Console.WriteLine($"- {kv.Key} → {kv.Value.RoomName} ({kv.Value.RoomType})"); // show exit info
            }
        }
    }
}
