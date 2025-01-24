using Bogus;
using Saas.Domain;
using Saas.Domain.Posts;
using Saas.Tests.Fakes.Common;

namespace Saas.Tests.Fakes;

public static class FakeChatRooms
{
    public static ChatRoom Generate(List<User>? participants = null, List<Message>? messages = null)
        => GetFaker(participants, messages).Generate();
    
    private static Faker<ChatRoom> GetFaker(
        List<User>? participants = null,
        List<Message>? messages = null)
    {
        return new PrivateFaker<ChatRoom>(new PrivateBinder())
            .UsePrivateConstructor()
            .RuleFor("_participants", _ => participants ?? [])
            .RuleFor("_messages", _ => messages ?? [])
            .RuleFor(x => x.Name, FakeTitles.TitleFaker);
    }
}