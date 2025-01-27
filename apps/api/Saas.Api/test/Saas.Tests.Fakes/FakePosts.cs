using Bogus;
using Saas.Domain;
using Saas.Tests.Fakes.Common;

namespace Saas.Tests.Fakes;

public static class FakePosts
{
    public static List<Post> GetForUser(User user, int count, List<Subject>? subjects = null) => 
        GetPostFaker(user, subjects).Generate(count);

    public static Faker<Post> GetPostFaker(
        User user,
        List<Subject>? subjects = null)
    {
        return new PrivateFaker<Post>()
            .UsePrivateConstructor()
            .UseSeed(user.GetHashCode())
            .RuleFor(x => x.Title, FakeTitles.TitleFaker.Generate())
            .RuleFor(x => x.Content, f => f.Lorem.Paragraph())
            .RuleFor(x => x.Slug, f => f.Lorem.Slug())
            .RuleFor(x => x.Author, user)
            .RuleFor(x => x.Subjects, subjects ?? [])
            .RuleFor(x => x.CreatedAt, f => f.Date.Past());
    }
}