using System;
using Xunit;
using FlatRoomGame.Commands;

namespace FlatRoomGame.Tests
{
    public class CommandInvokerTests
    {
        private class DummyCommand : ICommand
        {
            public bool Executed { get; private set; }
            public string Name => "dummy";
            public string Description => "A dummy test command.";
            public void Execute() => Executed = true;
        }

        // Helper wrapper to register commands with different names
        private class NamedCommand : ICommand
        {
            private readonly string _name;
            private readonly ICommand _inner;

            public NamedCommand(string name, ICommand inner)
            {
                _name = name;
                _inner = inner;
            }

            public string Name => _name;
            public string Description => _inner.Description;
            public void Execute() => _inner.Execute();
        }

        [Fact]
        public void ExecuteCommand_RegisteredCommand_ExecutesSuccessfully()
        {
            // Arrange
            var invoker = new CommandInvoker();
            var cmd = new DummyCommand();
            invoker.RegisterCommand(cmd);

            // Act
            var result = invoker.ExecuteCommand("dummy");

            // Assert
            Assert.False(result); // command returns false by default
            Assert.True(cmd.Executed);
        }

        [Fact]
        public void ExecuteCommand_UnregisteredCommand_ReturnsFalse()
        {
            // Arrange
            var invoker = new CommandInvoker();

            // Act
            var result = invoker.ExecuteCommand("nonexistent");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RegisterCommand_OverridesExistingCommandWithSameName()
        {
            // Arrange
            var invoker = new CommandInvoker();
            var first = new DummyCommand();
            var second = new DummyCommand();

            invoker.RegisterCommand(first);
            invoker.RegisterCommand(second); // overrides first

            // Act
            invoker.ExecuteCommand("dummy");

            // Assert
            Assert.False(first.Executed);
            Assert.True(second.Executed);
        }

        [Fact]
        public void ExecuteCommand_IgnoresCaseDifferences()
        {
            // Arrange
            var invoker = new CommandInvoker();
            var cmd = new DummyCommand();
            invoker.RegisterCommand(cmd);

            // Act
            var result = invoker.ExecuteCommand("DUMMY"); // uppercase

            // Assert
            Assert.False(result);
            Assert.True(cmd.Executed);
        }

        [Fact]
        public void MultipleCommands_CanExecuteIndependently()
        {
            // Arrange
            var invoker = new CommandInvoker();
            var cmd1 = new DummyCommand();
            var cmd2 = new DummyCommand();

            invoker.RegisterCommand(new NamedCommand("dummy1", cmd1));
            invoker.RegisterCommand(new NamedCommand("dummy2", cmd2));

            // Act
            var result1 = invoker.ExecuteCommand("dummy1");
            var result2 = invoker.ExecuteCommand("dummy2");

            // Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.True(cmd1.Executed);
            Assert.True(cmd2.Executed);
        }

        [Fact]
        public void ExecuteCommand_WithEmptyInput_ReturnsFalse()
        {
            // Arrange
            var invoker = new CommandInvoker();
            var cmd = new DummyCommand();
            invoker.RegisterCommand(cmd);

            // Act
            var result = invoker.ExecuteCommand(string.Empty);

            // Assert
            Assert.False(result);
            Assert.False(cmd.Executed);
        }

        [Fact]
        public void ExecuteCommand_WithWhitespaceInput_ReturnsFalse()
        {
            // Arrange
            var invoker = new CommandInvoker();
            var cmd = new DummyCommand();
            invoker.RegisterCommand(cmd);

            // Act
            var result = invoker.ExecuteCommand("   ");

            // Assert
            Assert.False(result);
            Assert.False(cmd.Executed);
        }

        [Fact]
        public void ExecuteCommand_PartialName_DoesNotExecute()
        {
            // Arrange
            var invoker = new CommandInvoker();
            var cmd = new DummyCommand();
            invoker.RegisterCommand(cmd);

            // Act
            var result = invoker.ExecuteCommand("dum"); // partial name

            // Assert
            Assert.False(result);
            Assert.False(cmd.Executed);
        }

        [Fact]
        public void RegisterCommand_MultipleTimes_UsesLastRegistered()
        {
            // Arrange
            var invoker = new CommandInvoker();
            var first = new DummyCommand();
            var second = new DummyCommand();

            invoker.RegisterCommand(first);
            invoker.RegisterCommand(second); // overrides first

            // Act
            invoker.ExecuteCommand("dummy");

            // Assert
            Assert.False(first.Executed);
            Assert.True(second.Executed);
        }
    }
}
