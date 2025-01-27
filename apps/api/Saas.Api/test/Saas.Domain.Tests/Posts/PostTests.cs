using Ardalis.Result;
using FluentAssertions;
using Saas.Tests.Fakes;

namespace Saas.Domain.Tests.Posts;

public class PostTests
{
    [Fact]
    public void Create_ShouldReturnInvalid_IfTitleIsTooLong()
    {
        // Arrange
        var title = new string(Random.Shared.GetItems("abcdef12345".ToCharArray(), 150));

        // Act
        var createResult = Post.Create(title, "Hello!", [], FakeUsers.Generate());

        // Assert
        createResult.IsSuccess.Should().BeFalse();
        createResult.IsInvalid().Should().BeTrue();

        createResult.ValidationErrors.Should().HaveCount(1);
    }
}