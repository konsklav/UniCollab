using Ardalis.Result;
using FluentAssertions;
using Saas.Tests.Fakes;

namespace Saas.Domain.Tests;

public class UserPostTests
{
    [Fact]
    public void CreatePost_Should_SuccessfullyCreatePost()
    {
        // Arrange
        var user = FakeUsers.Generate();
        
        // Act
        var result = user.CreatePost("Test", "Test", []);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}