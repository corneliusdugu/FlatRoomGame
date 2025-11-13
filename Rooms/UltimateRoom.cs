using FlatRoomGame.Characters;
using FlatRoomGame.Challenges;
using System;

namespace FlatRoomGame.Rooms
{
    // final boss room type
    public class UltimateRoom : BaseRoom
    {
        public UltimateRoom(string roomName, string trait, IChallenge challenge)
            : base(roomName, "Ultimate", trait, challenge) // set up base room
        {
        }

        public override void Enter(Player player)
        {
            Console.WriteLine($"🏆 Entering {RoomName} ({RoomType})"); // show enter message
            challenge.Execute(player); // run final challenge
        }
    }
}
