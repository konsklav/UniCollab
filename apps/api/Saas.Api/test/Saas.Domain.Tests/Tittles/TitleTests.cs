using Ardalis.Result;
using FluentAssertions;
using Saas.Domain.Common;

namespace Saas.Domain.Tests.Tittles;

public class TitleTests
{
    [Theory]
    [InlineData("UniCollab Rocks!")]
    [InlineData("UniCollab Reaches 1 Billion Monthly Users!")]
    [InlineData("UniCollab's Reign is Unstoppable, World Domination?")]
    public void Create_ShouldBeValid_ForShortTitles(string title)
    {
        // Act
        var titleResult = Title.Create(title);

        // Assert       
        titleResult.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ShouldBeInvalid_ForEmptyTitles(string title)
    {
        // Act
        var titleResult = Title.Create(title);
        
        // Assert
        titleResult.IsSuccess.Should().BeFalse();
        titleResult.IsInvalid().Should().BeTrue();

        titleResult.ValidationErrors.Should().Contain(err =>
            err.ErrorMessage.Contains("empty", StringComparison.CurrentCultureIgnoreCase));
    }

    [Fact]
    public void Create_ShouldBeInvalid_ForLongTitles()
    {
        // Arrange
        var title = new string(Enumerable.Range(0, 150).SelectMany(i => i.ToString()).ToArray());

        // Act
        var titleResult = Title.Create(title);

        // Assert
        titleResult.IsSuccess.Should().BeFalse();
        titleResult.IsInvalid().Should().BeTrue();

        titleResult.ValidationErrors.Should().Contain(err =>
            err.ErrorMessage.Contains(Title.MaxLength.ToString()));
    } 
}