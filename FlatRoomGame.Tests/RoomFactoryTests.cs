using Xunit;
using FlatRoomGame.Factories;
using FlatRoomGame.Rooms;

namespace FlatRoomGame.Tests
{
    public class RoomFactoryTests
    {
        private readonly FlatMysteryRoomFactory _factory = new FlatMysteryRoomFactory();

        [Fact]
        public void CreateSkillRoom_ReturnsSkillRoom()
        {
            // Arrange
            var roomName = "Room A";
            var trait = "Clever";

            // Act
            var room = _factory.CreateSkillRoom(roomName, trait);

            // Assert
            Assert.IsType<SkillRoom>(room);
            Assert.Equal("Room A", room.RoomName);
            Assert.Equal("Clever", room.Trait);
        }

        [Fact]
        public void CreatePhysicalRoom_ReturnsPhysicalRoom()
        {
            // Arrange
            var roomName = "Room B";
            var trait = "Strong";

            // Act
            var room = _factory.CreatePhysicalRoom(roomName, trait);

            // Assert
            Assert.IsType<PhysicalRoom>(room);
            Assert.Equal("Room B", room.RoomName);
            Assert.Equal("Strong", room.Trait);
        }

        [Fact]
        public void CreateUltimateRoom_ReturnsUltimateRoom()
        {
            // Arrange
            var roomName = "Room C";
            var trait = "Brave";

            // Act
            var room = _factory.CreateUltimateRoom(roomName, trait);

            // Assert
            Assert.IsType<UltimateRoom>(room);
            Assert.Equal("Room C", room.RoomName);
            Assert.Equal("Brave", room.Trait);
        }
    }
}
