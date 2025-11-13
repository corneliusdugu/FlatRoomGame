using FlatRoomGame.Characters;
using FlatRoomGame.Challenges;
using System;

namespace FlatRoomGame.Rooms
{
    // room type for combat challenges
    public class PhysicalRoom : BaseRoom
    {
        public PhysicalRoom(string roomName, string trait, IChallenge challenge)
            : base(roomName, "Physical", trait, challenge) // set up base room
        {
        }

        public override void Enter(Player player)
        {
            Console.WriteLine($"💪 Entering {RoomName} ({RoomType})"); // show enter message
            challenge.Execute(player); // start combat challenge
        }
    }
}
