using System.Text.Json.Serialization;
using Ardalis.Result;
using FluentAssertions;

namespace Saas.Domain.Tests.Users;

public class UserCreationTests
{
    [Fact]
    public void Invalid_WhenGoogleIdEmpty()
    {
        // Act
        var result = User.CreateWithGoogle("Makis", "");

        // Assert
        result.IsInvalid().Should().BeTrue();
        result.ValidationErrors.Should().HaveCount(1);
    }

    [Fact]
    public void Success_WhenGoogleIdIsValid()
    {
        // Arrange
        const string googleId = "108490894350753123351";

        // Act
        var result = User.CreateWithGoogle("Test", googleId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var user = result.Value;
        
        user.Friends.Should().BeEmpty();
        user.Posts.Should().BeEmpty();
        user.Password.Should().BeNull();
        user.GoogleId.Should().NotBeNull().And.Be(googleId);
    }

    [Fact]
    public void Invalid_WhenPasswordIsEmpty()
    {
        // Act
        var result = User.Create("Makis", "");

        // Assert
        result.IsInvalid().Should().BeTrue();
        result.ValidationErrors.Should().HaveCount(1);
    }

    [Fact]
    public void Success_WhenPasswordIsValid()
    {
        // Arrange
        const string password = "abc123";

        // Act
        var result = User.Create("Makis", password);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var user = result.Value;
        
        user.Friends.Should().BeEmpty();
        user.Posts.Should().BeEmpty();
        user.Password.Should().NotBeNull().And.Be(password);
        user.GoogleId.Should().BeNull();
    }
}