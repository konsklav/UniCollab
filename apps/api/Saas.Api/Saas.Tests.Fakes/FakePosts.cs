using Bogus;
using Saas.Domain;
using Saas.Domain.Posts;
using Saas.Tests.Fakes.Common;

namespace Saas.Tests.Fakes;

public static class FakePosts
{
    public static List<Post> GetForUser(User user, int count) => GetPostFaker(user).Generate(count);

    public static Faker<Post> GetPostFaker(User user) => new PrivateFaker<Post>()
        .UsePrivateConstructor()
        .UseSeed(user.GetHashCode())
        .RuleFor(x => x.Title, FakeTitles.TitleFaker.Generate())
        .RuleFor(x => x.Content, f => f.Lorem.Paragraph())
        .RuleFor(x => x.Slug, f => f.Lorem.Slug())
        .RuleFor(x => x.Author, user)
        .RuleFor(x => x.Subjects, [])
        .RuleFor(x => x.CreatedAt, f => f.Date.Past());


}