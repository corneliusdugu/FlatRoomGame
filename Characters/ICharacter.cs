namespace FlatRoomGame.Characters
{
    // interface for characters like player or enemy
    public interface ICharacter
    {
        string Name { get; } // character name
        int Health { get; set; } // character health

        void TakeDamage(int amount); // take damage
        bool IsAlive(); // check if still alive
    }
}
