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
        .RuleFor(x => x.Title, TitleFaker.Generate())
        .RuleFor(x => x.Content, f => f.Lorem.Paragraph())
        .RuleFor(x => x.Slug, (f, p) => 
            string.Join('-', p.Title.Value
                .ToLowerInvariant()
                .Split(' ')))
        .RuleFor(x => x.Author, user)
        .RuleFor(x => x.Subjects, [])
        .RuleFor(x => x.CreatedAt, f => f.Date.Past());

    private static readonly Faker<Title> TitleFaker = new PrivateFaker<Title>()
        .UsePrivateConstructor()
        .RuleFor(x => x.Value, f => f.Lorem.Sentence());
}