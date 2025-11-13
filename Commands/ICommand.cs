namespace FlatRoomGame.Commands
{
    // interface for all commands
    public interface ICommand
    {
        string Name { get; } // command name
        string Description { get; } // command info
        void Execute(); // run the command
    }
}
