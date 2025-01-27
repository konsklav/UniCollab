using Bogus;
using Saas.Domain.Common;
using Saas.Tests.Fakes.Common;

namespace Saas.Tests.Fakes;

public static class FakeTitles
{
    public static readonly Faker<Title> TitleFaker = new PrivateFaker<Title>()
        .UsePrivateConstructor()
        .RuleFor(x => x.Value, f => f.Lorem.Sentence(wordCount: 3, range: 1));
}