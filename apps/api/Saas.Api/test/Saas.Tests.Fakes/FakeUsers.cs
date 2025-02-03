using Bogus;
using Saas.Domain;
using Saas.Tests.Fakes.Common;

namespace Saas.Tests.Fakes;

public static class FakeUsers
{
    public static User Generate(
        List<User>? friends = null,
        List<Post>? posts = null)
    {
        var userFaker = GetFaker(friends, posts);

        return userFaker.Generate();
    }

    public static List<User> Generate(
        int count,
        List<User>? friends = null,
        List<Post>? posts = null)
    {
        var userFaker = GetFaker(friends, posts);

        return userFaker.Generate(count);
    }
    
    private static Faker<User> GetFaker(
        List<User>? friends = null,
        List<Post>? posts = null)
    {
        return new PrivateFaker<User>(new PrivateBinder())
            .UsePrivateConstructor()
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor("_friends", _ => friends ?? [])
            .RuleFor("_posts", _ => posts ?? []);
    }
}