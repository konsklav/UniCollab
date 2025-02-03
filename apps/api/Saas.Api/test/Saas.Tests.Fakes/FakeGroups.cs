using Bogus;
using Saas.Domain;
using Saas.Tests.Fakes.Common;

namespace Saas.Tests.Fakes;

public static class FakeGroups
{
    public static Group Generate(List<User>? members = null, User? creator = null)
        => GetFaker(members, creator).Generate();
    
    private static Faker<Group> GetFaker(
        List<User>? members = null,
        User? creator = null)
    {
        return new PrivateFaker<Group>(new PrivateBinder())
            .UsePrivateConstructor()
            .RuleFor("_members", _ => members ?? [])
            .RuleFor(x => x.Creator, _ => creator)
            .RuleFor(x => x.Name, FakeTitles.TitleFaker);
    }
}