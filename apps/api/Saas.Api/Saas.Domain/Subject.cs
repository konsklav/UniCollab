using Saas.Domain.Common;

namespace Saas.Domain;

public class Subject(string name, Guid? id = null) : Entity(id)
{
    private string Name { get; } = name;
}