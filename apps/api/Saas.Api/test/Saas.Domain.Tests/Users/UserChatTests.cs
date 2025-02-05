using FluentAssertions;
using Saas.Tests.Fakes;

namespace Saas.Domain.Tests.Users;

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
        var chatroom = FakeChatRooms.Generate(participants: [user]);

        var expectedMessage = new Message("Test", default, user);
        
        // Act
        var result = user.SendMessage(chatroom, "Test");
        
        // Assert
        chatroom.Messages.Should().Contain(msg => msg.Content == expectedMessage.Content &&
                                                  msg.Sender == user);
        
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