using FluentAssertions;
using Saas.Domain.Common;

namespace Saas.Domain.Tests.Common;

public class SlugHelperTests
{
    [Theory]
    [InlineData("Hello There!", "hello-there")]
    [InlineData("Numb3rs 4r3 n0t Ignored!@#", "numb3rs-4r3-n0t-ignored")]
    [InlineData(" Leading or Trailing   whitespace IGNORED!  ", "leading-or-trailing-whitespace-ignored")]
    public void ShouldGenerateSlugs_InKebabCase_WithoutSpecialCharacters(string title, string expected)
    {
        // Act
        var result = SlugHelper.Get(title);

        // Assert
        result.Should().Be(expected);
    }
}