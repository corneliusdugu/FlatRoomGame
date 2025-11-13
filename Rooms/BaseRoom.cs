using System.Collections.Generic;
using FlatRoomGame.Characters;
using FlatRoomGame.Challenges;

namespace FlatRoomGame.Rooms
{
    // base class for all room types
    public abstract class BaseRoom : IRoom
    {
        public string RoomName { get; protected set; } // name of room
        public string RoomType { get; protected set; } // type of room
        public string Trait { get; protected set; } // trait linked to room
        public bool IsCleared { get; protected set; } // if room done
        public Dictionary<string, IRoom> Exits { get; } = new(); // exits to other rooms

        protected readonly IChallenge challenge; // room challenge

        protected BaseRoom(string roomName, string roomType, string trait, IChallenge challenge)
        {
            RoomName = roomName; // set name
            RoomType = roomType; // set type
            Trait = trait; // set trait
            this.challenge = challenge; // set challenge
            IsCleared = false; // not cleared yet
        }

        public void Connect(string direction, IRoom target)
        {
            if (!string.IsNullOrWhiteSpace(direction) && target != null) // check direction and target
            {
                Exits[direction.ToLower()] = target; // add to exits
            }
        }

        public void SetCleared(bool cleared = true) => IsCleared = cleared; // mark as cleared

        public abstract void Enter(Player player); // enter room, must be implemented
    }
}
