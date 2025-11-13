using System;
using System.Collections.Generic;

namespace FlatRoomGame.Commands
{
    // handles registering and running commands
    public class CommandInvoker
    {
        private readonly Dictionary<string, ICommand> _commands = new(); // list of commands

        public void RegisterCommand(ICommand command)
        {
            _commands[command.Name.ToLower()] = command; // store command by name
        }

        public bool ExecuteCommand(string input)
        {
            if (_commands.TryGetValue(input.ToLower(), out var command)) // check if command exists
            {
                command.Execute(); // run command

                // only return true for certain commands that move game forward
                var name = input.ToLower();
                if (name is "attack" or "solve" or "north" or "south" or "east" or "west" or "forward" or "back" or "left" or "right")
                {
                    return true; // valid flow command
                }
                return false; // not a flow command
            }

            Console.WriteLine("❓ Command not recognized. Type 'help' for options."); // invalid command
            return false; // failed
        }

        public void ShowAvailableCommands()
        {
            Console.WriteLine("\n📜 Available Commands:"); // show all commands
            foreach (var cmd in _commands.Values)
            {
                Console.WriteLine($"- {cmd.Name} : {cmd.Description}"); // list command and info
            }
        }
    }
}
