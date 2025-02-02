using Ardalis.Result;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Saas.Application.Interfaces.Data;
using Saas.Application.UseCases.Groups;
using Saas.Domain;
using Saas.Tests.Fakes;

namespace Saas.Application.Tests.UseCases.Groups;

public class JoinGroupTests
{
    private readonly JoinGroup _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IGroupRepository _groupRepository = Substitute.For<IGroupRepository>();

    private readonly User _user;
    private readonly Group _group;
    
    public JoinGroupTests()
    {
        _user = FakeUsers.Generate();
        _group = FakeGroups.Generate();
        _sut = new JoinGroup(_userRepository, _groupRepository);
    }

    [Fact]
    public async Task NotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepository.GetByIdAsync(_user.Id).ReturnsNull();

        // Act
        var result = await _sut.HandleAsync(_group.Id, _user.Id);

        // Assert
        result.IsNotFound().Should().BeTrue();
        await _userRepository.Received(1).GetByIdAsync(_user.Id);
        await _groupRepository.Received(0).GetByIdAsync(Arg.Any<Guid>());
    }

    [Fact]
    public async Task NotFound_WhenGroupDoesNotExist()
    {
        // Arrange
        _userRepository.GetByIdAsync(_user.Id).Returns(_user);
        _groupRepository.GetByIdAsync(_group.Id).ReturnsNull();

        // Act
        var result = await _sut.HandleAsync(_group.Id, _user.Id);

        // Assert
        result.IsNotFound().Should().BeTrue();
        result.Errors.Should().Contain(err => err.Contains(_group.Id.ToString()));

        await _userRepository.Received(1).GetByIdAsync(_user.Id);
        await _groupRepository.Received(1).GetByIdAsync(_group.Id);
    }

    [Fact]
    public async Task Conflict_WhenUserAlreadyInGroup()
    {
        // Arrange
        var group = FakeGroups.Generate(members: [_user]);
        _userRepository.GetByIdAsync(_user.Id).Returns(_user);
        _groupRepository.GetByIdAsync(group.Id).Returns(group);

        // Act
        var result = await _sut.HandleAsync(group.Id, _user.Id);

        // Assert
        result.IsConflict().Should().BeTrue();
    }

    [Fact]
    public async Task Success_WhenUserIsNotInGroup()
    {
        // Arrange
        _userRepository.GetByIdAsync(_user.Id).Returns(_user);
        _groupRepository.GetByIdAsync(_group.Id).Returns(_group);

        // Act
        var result = await _sut.HandleAsync(_group.Id, _user.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}