using FlatRoomGame.Rooms;

namespace FlatRoomGame.Factories
{
    // interface for making different kinds of rooms
    public interface IRoomFactory
    {
        IRoom CreateSkillRoom(string name, string trait); // make puzzle room
        IRoom CreatePhysicalRoom(string name, string trait); // make combat room
        IRoom CreateUltimateRoom(string name, string trait); // make final boss room
    }
}
