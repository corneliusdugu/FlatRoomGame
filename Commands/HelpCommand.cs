using System;

namespace FlatRoomGame.Commands
{
    // command to show help list
    public class HelpCommand : ICommand
    {
        private readonly CommandInvoker _invoker; // command handler

        public HelpCommand(CommandInvoker invoker)
        {
            _invoker = invoker; // set invoker
        }

        public string Name => "help"; // command name
        public string Description => "Show available commands."; // command info

        public void Execute()
        {
            _invoker.ShowAvailableCommands(); // show all commands
        }
    }
}
