using Bogus;
using Saas.Domain;
using Saas.Domain.Posts;

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
        return new Faker<User>().CustomInstantiator(f =>
        {
            var username = f.Internet.UserName();
            var password = f.Internet.Password();
            var friendList = friends ?? [];
            var postList = posts ?? [];

            return new User(username, password, friendList, postList);
        });
    }
}