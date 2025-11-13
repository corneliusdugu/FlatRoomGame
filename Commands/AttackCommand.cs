using FlatRoomGame.Characters;
using FlatRoomGame.Challenges;
using System;

namespace FlatRoomGame.Commands
{
    // command to start combat attack
    public class AttackCommand : ICommand
    {
        private readonly Player _player; // player object
        private readonly CombatChallenge _challenge; // combat challenge
        private readonly Action<bool>? _onCompleted; // optional callback when done

        // old way to make command (no callback)
        public AttackCommand(Player player, CombatChallenge challenge)
            : this(player, challenge, null) { } // call main constructor

        // new way with optional callback
        public AttackCommand(Player player, CombatChallenge challenge, Action<bool>? onCompleted)
        {
            _player = player; // set player
            _challenge = challenge; // set challenge
            _onCompleted = onCompleted; // set callback
        }

        public string Name => "attack"; // command name
        public string Description => "Attack the enemy in combat."; // command info

        public void Execute()
        {
            bool success = _challenge.Execute(_player); // run fight
            _onCompleted?.Invoke(success); // run callback if there is one
        }
    }
}
