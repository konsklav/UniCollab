using Ardalis.Result;
using FluentAssertions;
using Saas.Domain.Common;
using Saas.Tests.Fakes;

namespace Saas.Domain.Tests.ChatRooms;

public class ChatRoomCreationTests
{
    [Fact]
    public void Invalid_WhenNameIsTooLong()
    {
        // Arrange
        var name = new string(Enumerable.Range(0, Title.MaxLength + 1).Select(_ => 'A').ToArray());

        // Act
        var result = ChatRoom.Create(name, FakeUsers.Generate(1));

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsInvalid().Should().BeTrue();

        result.ValidationErrors.Should().HaveCount(1);
        result.ValidationErrors.Should().Contain(err => err.ErrorMessage.Contains($"{Title.MaxLength}"));
    }

    [Fact]
    public void Invalid_CreatingWithNoUsers()
    {
        // Act
        var result = ChatRoom.Create("Test!", []);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsInvalid().Should().BeTrue();

        result.ValidationErrors.Should().HaveCount(1);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("              ")]
    public void Invalid_WhenNameIsEmptyOrWhitespace(string name)
    {
        // Arrange
        var users = FakeUsers.Generate(2);

        // Act
        var result = ChatRoom.Create(name, users);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsInvalid().Should().BeTrue();
        
        result.ValidationErrors.Should().HaveCount(1);
    }
}