using System;
using FlatRoomGame.Characters;

namespace FlatRoomGame.Challenges
{
    // class for combat challenge between player and enemy
    public class CombatChallenge : IChallenge
    {
        private readonly Enemy enemy; // enemy object
        private readonly Random rng = new Random(); // random number for damage

        public CombatChallenge(string enemyName, int health)
        {
            enemy = new Enemy(enemyName, health); // make new enemy
        }

        public bool Execute(Player player)
        {
            Console.WriteLine($"⚔️ You are fighting {enemy.Name}!"); // show who you fight

            while (player.IsAlive() && enemy.IsAlive()) // keep fighting till someone dies
            {
                Console.WriteLine("Press Enter or type 'attack' to strike...");
                string? input = Console.ReadLine()?.Trim().ToLower(); // get player input

                if (input == "attack" || input == string.Empty) // if player attacks
                {
                    int playerDamage = rng.Next(12, 19); // random player damage
                    enemy.TakeDamage(playerDamage); // hit enemy

                    if (enemy.IsAlive()) // if enemy still alive
                    {
                        int enemyDamage = rng.Next(8, 13); // random enemy damage
                        player.TakeDamage(enemyDamage); // hit player
                    }

                    // show both health
                    Console.WriteLine($"📊 Status → {player.Name}: {player.Health} HP | {enemy.Name}: {enemy.Health} HP\n");
                }
                else
                {
                    Console.WriteLine("❌ Invalid input. Type 'attack' or press Enter."); // wrong input
                }
            }

            if (player.IsAlive()) // player won
            {
                Console.WriteLine("🏆 You won the battle!");
                return true;
            }

            Console.WriteLine("💀 You were defeated!"); // player lost
            return false;
        }
    }
}
