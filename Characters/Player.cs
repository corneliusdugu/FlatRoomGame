using System;

namespace FlatRoomGame.Characters
{
    // player class that holds player info and stats
    public class Player : ICharacter
    {
        public string Name { get; private set; } // player name
        public string Gender { get; private set; } // player gender
        public string Trait { get; private set; } // player trait
        public int Health { get; set; } = 100; // player health

        public Player(string name, string gender, string trait)
        {
            Name = name; // set name
            Gender = gender; // set gender
            Trait = trait; // set trait
        }

        public void TakeDamage(int amount)
        {
            Health -= amount; // subtract damage
            Console.WriteLine($"{Name} took {amount} damage. Remaining health: {Health}"); // show health
        }

        public bool IsAlive()
        {
            return Health > 0; // true if health above 0
        }
    }
}
