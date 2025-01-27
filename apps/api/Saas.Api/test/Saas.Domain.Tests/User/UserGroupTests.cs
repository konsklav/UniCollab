using FluentAssertions;
using Saas.Tests.Fakes;

namespace Saas.Domain.Tests.User;

public class UserGroupTests
{
    [Fact]
    public void JoinGroup_Should_SuccessfullyAddUserToGroup()
    {
        // Arrange
        var user1 = FakeUsers.Generate();
        var user2 = FakeUsers.Generate();
        
        var group = new Group("Test", [user1], user1);
        
        // Act
        var result = user2.JoinGroup(group);
        
        // Assert
        group.Members.Should().Contain(user2);
        
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void LeaveGroup_Should_SuccessfullyRemoveUserFromGroup()
    {
        // Arrange
        var user1 = FakeUsers.Generate();
        var user2 = FakeUsers.Generate();
        
        var group = new Group("Test", [user1, user2], user1);
        
        // Act
        var result = user2.LeaveGroup(group);
        
        // Assert
        group.Members.Should().NotContain(user2);
        
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void CreateGroup_Should_SuccessfullyCreateGroup()
    {
        // Arrange
        var user = FakeUsers.Generate();
        
        // Act
        var result = user.CreateGroup("test");
        
        // Assert
        // Idk what to do!
    }
}