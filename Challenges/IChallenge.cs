using FlatRoomGame.Characters;

namespace FlatRoomGame.Challenges
{
    // interface for all challenge types
    public interface IChallenge
    {
        bool Execute(Player player); // run the challenge and return true if passed
    }
}
