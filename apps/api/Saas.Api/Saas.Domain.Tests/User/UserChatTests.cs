using FluentAssertions;
using Saas.Tests.Fakes;

namespace Saas.Domain.Tests.User;

public class UserChatTests
{
    [Fact]
    public void JoinChat_Should_SuccessfullyAddUserToChat()
    {
        // Arrange
        var user1 = FakeUsers.Generate();
        var user2 = FakeUsers.Generate();
        
        var chatroom = FakeChatRooms.Generate(participants: [user1]);
        
        // Act
        var result = user2.JoinChat(chatroom);
        
        // Assert
        chatroom.Participants.Should().Contain(user2);
        
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void SendMessage_Should_SuccessfullyAddMessageToChat()
    {
        // Arrange
        var user = FakeUsers.Generate();
        var message = new Message("Test", DateTime.Now, user);
        var chatroom = FakeChatRooms.Generate(participants: [user]);
        
        // Act
        var result = user.SendMessage(chatroom, message);
        
        // Assert
        chatroom.Messages.Should().Contain(message);
        
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void DeleteMessage_Should_SuccessfullyRemoveMessageFromChat()
    {
        // Arrange 
        var user = FakeUsers.Generate();
        var message = new Message("Test", DateTime.Now, user);
        var chatroom = FakeChatRooms.Generate(participants: [user], messages: [message]);
        
        // Act
        var result = user.DeleteMessage(chatroom, message);
        
        // Assert
        chatroom.Messages.Should().NotContain(message);
        
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void LeaveChat_Should_SuccessfullyRemoveUserFromChat()
    {
        // Arrange
        var user = FakeUsers.Generate();
        var chatroom = FakeChatRooms.Generate(participants: [user]);
        
        // Act
        var result = user.LeaveChat(chatroom);
        
        // Assert
        chatroom.Participants.Should().NotContain(user);
        
        result.IsSuccess.Should().BeTrue();
    }
}