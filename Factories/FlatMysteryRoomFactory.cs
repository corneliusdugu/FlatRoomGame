using FlatRoomGame.Challenges;
using FlatRoomGame.Rooms;

namespace FlatRoomGame.Factories
{
    // factory to make different types of rooms
    public class FlatMysteryRoomFactory : IRoomFactory
    {
        public IRoom CreateSkillRoom(string name, string trait)
        {
            return new SkillRoom(name, trait, new MathPuzzle()); // make puzzle room
        }

        public IRoom CreatePhysicalRoom(string name, string trait)
        {
            return new PhysicalRoom(name, trait, new CombatChallenge("Enemy", 40)); // make combat room
        }

        public IRoom CreateUltimateRoom(string name, string trait)
        {
            return new UltimateRoom(name, trait, new CombatChallenge("Master", 70)); // make boss room
        }
    }
}
