using FlatRoomGame.Characters;
using FlatRoomGame.Challenges;
using System;

namespace FlatRoomGame.Commands
{
    // command to solve a puzzle
    public class SolveCommand : ICommand
    {
        private readonly Player _player; // player ref
        private readonly MathPuzzle _puzzle; // puzzle ref
        private readonly Action<bool>? _onCompleted; // optional callback when done

        // old version without callback
        public SolveCommand(Player player, MathPuzzle puzzle)
            : this(player, puzzle, null) { } // call main one

        // new version with callback
        public SolveCommand(Player player, MathPuzzle puzzle, Action<bool>? onCompleted)
        {
            _player = player; // set player
            _puzzle = puzzle; // set puzzle
            _onCompleted = onCompleted; // set callback
        }

        public string Name => "solve"; // command name
        public string Description => "Solve the puzzle challenge."; // command info

        public void Execute()
        {
            bool success = _puzzle.Execute(_player); // run puzzle
            _onCompleted?.Invoke(success); // run callback if any
        }
    }
}
