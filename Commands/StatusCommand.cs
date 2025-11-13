using FlatRoomGame.Characters;
using System;

namespace FlatRoomGame.Commands
{
    // command to show player status
    public class StatusCommand : ICommand
    {
        private readonly Player _player; // player ref

        public StatusCommand(Player player)
        {
            _player = player; // set player
        }

        public string Name => "status"; // command name
        public string Description => "Check your current health and status."; // command info

        public void Execute()
        {
            Console.WriteLine($"📊 {_player.Name}'s Status:"); // show player name
            Console.WriteLine($"Health: {_player.Health}"); // show health
            Console.WriteLine($"Searching for: {_player.Trait}"); // show player trait
        }
    }
}
