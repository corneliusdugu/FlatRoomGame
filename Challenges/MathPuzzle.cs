using System;
using FlatRoomGame.Characters;

namespace FlatRoomGame.Challenges
{
    // class for a simple math puzzle challenge
    public class MathPuzzle : IChallenge
    {
        public bool Execute(Player player)
        {
            Console.Clear(); // clear screen
            Random rng = new Random(); // make random number generator

            int a = rng.Next(1, 20); // first number
            int b = rng.Next(1, 20); // second number
            int correctAnswer = a + b; // right answer

            Console.WriteLine("🧠 Solve this math puzzle to proceed!"); // show message
            Console.Write($"What is {a} + {b}? "); // ask question
            string? input = Console.ReadLine(); // read input

            if (int.TryParse(input, out int answer) && answer == correctAnswer) // check answer
            {
                Console.WriteLine("✅ Correct!"); // right
                return true; // pass
            }

            Console.WriteLine($"❌ Wrong answer! The correct answer was {correctAnswer}."); // wrong answer

            Console.WriteLine("\nWould you like to try again? (Y/N)"); // ask to retry
            string? retry = Console.ReadLine()?.Trim().ToUpper(); // get retry input

            if (retry == "Y") // if yes
            {
                return Execute(player); // call again
            }

            return false; // fail
        }
    }
}
