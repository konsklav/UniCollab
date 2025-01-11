using Ardalis.Result;
using FluentAssertions;

namespace Saas.Domain.Tests;

public class ChatRoomTests
{
    [Fact]
    public void AddParticipant_Should_AddUserToTheParticipantsList()
    {
        // Arrange
        var user1 = new User(Guid.NewGuid(), "Test", "password", []);
        var user2 = new User(Guid.NewGuid(), "Test 2", "password", [user1]);

        var chatRoom = new ChatRoom(Guid.NewGuid(), "test", "test", [user2], []);   // I just added user2 in there as a participant
        var initialparticipantsCount = chatRoom.Participants.Count;
        
        // Act
        var result = chatRoom.AddParticipant(user1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        chatRoom.Participants.Should().Contain(user1);
        chatRoom.Participants.Count.Should().Be(initialparticipantsCount + 1);
    }

    [Fact]
    public void AddParticipant_Should_ReturnConflict_WhenUserIsAlreadyAParticipant()
    {
        // Arrange
        var user1 = new User(Guid.NewGuid(), "Test", "password", []);
        var user2 = new User(Guid.NewGuid(), "Test 2", "password", [user1]);

        var chatRoom = new ChatRoom(Guid.NewGuid(), "test", "test", [user1, user2], []);
        
        // Act
        var result = chatRoom.AddParticipant(user1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsConflict().Should().BeTrue();
    }

    [Fact]
    public void RemoveParticipant_Should_RemoveParticipantFromTheParticipantsList()
    {
        // Arrange
        var user1 = new User(Guid.NewGuid(), "Test", "password", []);

        var chatRoom = new ChatRoom(Guid.NewGuid(), "test", "test", [user1], []);
        
        var initialparticipantsCount = chatRoom.Participants.Count;
        
        // Act
        var result = chatRoom.RemoveParticipant(user1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        chatRoom.Participants.Should().NotContain(user1);
        chatRoom.Participants.Count.Should().Be(initialparticipantsCount - 1);
    }

    [Fact]
    public void RemoveParticipant_Should_ReturnNotFound_WhenUserIsNotInParticipantsList()
    {
        // Arrange
        var user1 = new User(Guid.NewGuid(), "Test", "password", []);
        var user2 = new User(Guid.NewGuid(), "Test 2", "password", [user1]);

        var chatRoom = new ChatRoom(Guid.NewGuid(), "test", "test", [user2], []);   // I just added user2 in there as a participant
        
        // Act
        var result = chatRoom.RemoveParticipant(user1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsNotFound().Should().BeTrue();
    }

    [Fact]
    public void AddMessage_Should_AddMessageToTheMessagesList()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test", "password", []);
        
        var message = new Message("Test", DateTime.Now, user);
        
        var chatRoom = new ChatRoom(Guid.NewGuid(), "test", "test", [user], []);
        
        var initialMessagesCount = chatRoom.Messages.Count;
        
        // Act
        var result = chatRoom.AddMessage(message);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        
        chatRoom.Messages.Should().Contain(message);
        chatRoom.Messages.Count.Should().Be(initialMessagesCount + 1);
    }

    [Fact]
    public void AddMessage_Should_ReturnConflict_WhenMessageIsAlreadyInMessagesList()
    {
        // Arrange
        var user = new User(Guid.NewGuid(), "Test", "password", []);

        var message = new Message("Test", DateTime.Now, user);
        
        var chatRoom = new ChatRoom(Guid.NewGuid(), "test", "test", [user], [message]);
        
        // Act
        var result = chatRoom.AddMessage(message);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsConflict().Should().BeTrue();
    }
}