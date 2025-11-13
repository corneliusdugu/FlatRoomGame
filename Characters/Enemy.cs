using System;

namespace FlatRoomGame.Characters
{
    // enemy class that can take damage and has health
    public class Enemy : ICharacter
    {
        public string Name { get; private set; } // enemy name
        public int Health { get; set; } // enemy health

        public Enemy(string name, int health = 50)
        {
            Name = name; // set name
            Health = health; // set health
        }

        public void TakeDamage(int amount)
        {
            Health -= amount; // subtract damage from health
            Console.WriteLine($"{Name} took {amount} damage. Remaining health: {Health}"); // show damage info
        }

        public bool IsAlive()
        {
            return Health > 0; // true if health above 0
        }
    }
}
