using Ardalis.Result;
using FluentAssertions;
using Saas.Domain.Common;
using Saas.Tests.Fakes;

namespace Saas.Domain.Tests;

public class GroupCreationTests
{
    [Fact]
    public void Invalid_WhenNameIsTooLong()
    {
        // Arrange
        var name = new string(Enumerable.Range(0, Title.MaxLength + 1).Select(_ => 'A').ToArray());

        // Act
        var result = Group.Create(name, FakeUsers.Generate(2), FakeUsers.Generate());

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsInvalid().Should().BeTrue();

        result.ValidationErrors.Should().HaveCount(1);
        result.ValidationErrors.Should().Contain(err => err.ErrorMessage.Contains($"{Title.MaxLength}"));
    }

    [Fact]
    public void Invalid_CreatingWithNoMembers()
    {
        // Act
        var result = Group.Create("Test!", [], FakeUsers.Generate());

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
        var result = Group.Create(name, users, FakeUsers.Generate());

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsInvalid().Should().BeTrue();
        
        result.ValidationErrors.Should().HaveCount(1);
    }
}