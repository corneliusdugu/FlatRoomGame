using FlatRoomGame.Characters;
using FlatRoomGame.Challenges;
using System;

namespace FlatRoomGame.Rooms
{
    // room type for puzzle challenges
    public class SkillRoom : BaseRoom
    {
        public SkillRoom(string roomName, string trait, IChallenge challenge)
            : base(roomName, "Skill", trait, challenge) // set up base room
        {
        }

        public override void Enter(Player player)
        {
            Console.WriteLine($"🧠 Entering {RoomName} ({RoomType})"); // show enter message
            challenge.Execute(player); // run puzzle challenge
        }
    }
}
