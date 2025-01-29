using System.Xml.Serialization;
using Ardalis.Result;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Saas.Application.Interfaces.Data;
using Saas.Application.UseCases.ChatRooms;
using Saas.Domain;
using Saas.Tests.Fakes;

namespace Saas.Application.Tests.UseCases.Chat;

public class JoinChatTests
{
    private readonly JoinChatRoom _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IChatRoomRepository _chatRepository = Substitute.For<IChatRoomRepository>();

    private readonly User _user;
    private readonly ChatRoom _chat;
    
    public JoinChatTests()
    {
        _user = FakeUsers.Generate();
        _chat = FakeChatRooms.Generate();
        _sut = new JoinChatRoom(_userRepository, _chatRepository);
    }

    [Fact]
    public async Task NotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepository.GetByIdAsync(_user.Id).ReturnsNull();

        // Act
        var result = await _sut.HandleAsync(_chat.Id, _user.Id);

        // Assert
        result.IsNotFound().Should().BeTrue();
    }

    [Fact]
    public async Task NotFound_WhenChatDoesNotExist()
    {
        // Arrange
        _userRepository.GetByIdAsync(_user.Id).Returns(_user);
        _chatRepository.GetByIdAsync(_chat.Id).ReturnsNull();

        // Act
        var result = await _sut.HandleAsync(_chat.Id, _user.Id);

        // Assert
        result.IsNotFound().Should().BeTrue();
        result.Errors.Should().Contain(err => err.Contains(_chat.Id.ToString()));
    }

    [Fact]
    public async Task Conflict_WhenUserAlreadyInChat()
    {
        // Arrange
        var chat = FakeChatRooms.Generate(participants: [_user]);
        _userRepository.GetByIdAsync(_user.Id).Returns(_user);
        _chatRepository.GetByIdAsync(chat.Id).Returns(chat);

        // Act
        var result = await _sut.HandleAsync(chat.Id, _user.Id);

        // Assert
        result.IsConflict().Should().BeTrue();
    }

    [Fact]
    public async Task Success_WhenUserIsNotInChat()
    {
        // Arrange
        _userRepository.GetByIdAsync(_user.Id).Returns(_user);
        _chatRepository.GetByIdAsync(_chat.Id).Returns(_chat);

        // Act
        var result = await _sut.HandleAsync(_chat.Id, _user.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}