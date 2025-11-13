using System.Collections.Generic;
using FlatRoomGame.Characters;

namespace FlatRoomGame.Rooms
{
    // interface for all room types
    public interface IRoom
    {
        string RoomName { get; } // name of room
        string RoomType { get; } // type of room
        string Trait { get; } // trait linked to room
        bool IsCleared { get; } // if room is done
        Dictionary<string, IRoom> Exits { get; } // exits to other rooms
        void Enter(Player player); // what happens when player enters
    }
}
