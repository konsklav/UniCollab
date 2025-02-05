using Saas.Application.Models;

namespace Saas.Api.Contracts;

public sealed record SubjectCountDto(string Name, int Count)
{
    internal static SubjectCountDto From(SubjectCount subjectCount) =>
        new(Name: subjectCount.Subject.Name,
            Count: subjectCount.Count);
}