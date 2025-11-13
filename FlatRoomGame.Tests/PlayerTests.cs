using Xunit;
using FlatRoomGame.Characters;

namespace FlatRoomGame.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Player_Initializes_WithCorrectValues()
        {
            // Arrange
            var playerName = "Alice";
            var gender = "Female";
            var trait = "Brave";

            // Act
            var player = new Player(playerName, gender, trait);

            // Assert
            Assert.Equal("Alice", player.Name);
            Assert.Equal("Female", player.Gender);
            Assert.Equal("Brave", player.Trait);
            Assert.Equal(100, player.Health);
        }

        [Fact]
        public void TakeDamage_Reduces_Health()
        {
            // Arrange
            var player = new Player("Bob", "Male", "Strong");

            // Act
            player.TakeDamage(30);

            // Assert
            Assert.Equal(70, player.Health);
        }

        [Fact]
        public void IsAlive_ReturnsFalse_WhenHealthIsZeroOrBelow()
        {
            // Arrange
            var player = new Player("Charlie", "Male", "Clever");

            // Act
            player.TakeDamage(100);

            // Assert
            Assert.False(player.IsAlive());
        }

        [Fact]
        public void IsAlive_ReturnsTrue_WhenHealthAboveZero()
        {
            // Arrange
            var player = new Player("Diana", "Female", "Loyal");

            // Act
            player.TakeDamage(50);

            // Assert
            Assert.True(player.IsAlive());
        }
    }
}
