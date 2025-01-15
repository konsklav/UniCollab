using Ardalis.Result;
using FluentAssertions;

namespace Saas.Domain.Tests;

public class GroupTests
{
    [Fact]
    public void AddMember_Should_AddUserToMembersList()
    {
        // Arrange
        var user1 = new User("Test", "password", []);
        var user2 = new User("Test 2", "password", [user1]);
        
        var group = new Group("test", [user2], user2); // I supposed user2 is already a member since he's the creator (?)

        var initialMembersCount = group.Members.Count;
        
        // Act
        var result = group.AddMember(user1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        group.Members.Should().Contain(user1);
        group.Members.Count.Should().Be(initialMembersCount + 1);
    }

    [Fact]
    public void AddMember_Should_ReturnConflict_WhenUserIsAlreadyAMember()
    {
        // Arrange
        var user1 = new User("Test", "password", []);
        var user2 = new User("Test 2", "password", [user1]);
        var group = new Group("test", [user1, user2], user2);
        
        // Act
        var result = group.AddMember(user1);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsConflict().Should().BeTrue();
    }
    
    [Fact]
    public void RemoveMember_Should_RemoveUserFromMembersList()
    {
        // Arrange
        var user1 = new User("Test", "password", []);
        var user2 = new User("Test 2", "password", [user1]);
        
        var group = new Group("test", [user1, user2], user2);

        var initialMembersCount = group.Members.Count;
        
        // Act
        var result = group.RemoveMember(user1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        group.Members.Should().NotContain(user1);
        group.Members.Count.Should().Be(initialMembersCount - 1);
    }
    
    [Fact]
    public void RemoveMember_Should_ReturnNotFound_WhenUserIsNotInMembersList()
    {
        // Arrange
        var user1 = new User("Test", "password", []);
        var user2 = new User("Test 2", "password", [user1]);
        var group = new Group("test", [user2], user2);
        
        // Act
        var result = group.RemoveMember(user1);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsNotFound().Should().BeTrue();
    }
}