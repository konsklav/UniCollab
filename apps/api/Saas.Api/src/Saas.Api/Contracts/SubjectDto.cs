using Saas.Domain;

namespace Saas.Api.Contracts;

public sealed record SubjectDto(Guid Id, string Name)
{
    internal static SubjectDto From(Subject subject) =>
        new(Id: subject.Id,
            Name: subject.Name);
}