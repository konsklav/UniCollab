using Ardalis.Result;
using FluentAssertions;
using Saas.Tests.Fakes;

namespace Saas.Domain.Tests.Users;

public class UserFriendTests
{
    [Fact]
    public void AddFriend_Should_AddUserToFriendList()
    {
        // Arrange
        var user1 = FakeUsers.Generate();
        var user2 = FakeUsers.Generate();

        var initialFriendCount = user1.Friends.Count;

        // Act
        var result = user1.AddFriend(user2);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        user1.Friends.Should().Contain(user2);
        user1.Friends.Count.Should().Be(initialFriendCount + 1);
    }

    [Fact]
    public void RemoveFriend_Should_RemoveUserFromFriendList()
    {
        // Arrange
        var user2 = FakeUsers.Generate();
        var user1 = FakeUsers.Generate(friends: [user2]);

        // Act
        var result = user1.RemoveFriend(user2);

        // Assert
        result.IsSuccess.Should().BeTrue();
        user1.Friends.Should().BeEmpty();
    }

    [Fact]
    public void RemoveFriend_ShouldReturnNotFound_WhenUserIsNotInFriendList()
    {
        // Arrange
        var user1 = FakeUsers.Generate();
        var user2 = FakeUsers.Generate();

        // Act
        var result = user1.RemoveFriend(user2);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsNotFound().Should().BeTrue();
    }

    [Fact]
    public void AddFriend_ShouldReturnInvalid_WhenUserAddsThemselves()
    {
        // Arrange
        var user = FakeUsers.Generate();

        // Act
        var result = user.AddFriend(user);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsInvalid().Should().BeTrue();
    }

    [Fact]
    public void AddFriend_ShouldReturnConflict_WhenUserIsAlreadyAFriend()
    {
        // Arrange
        var user2 = FakeUsers.Generate();
        var user1 = FakeUsers.Generate(friends: [user2]);

        // Act
        var result = user1.AddFriend(user2);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsConflict().Should().BeTrue();
    }
}