namespace Saas.Domain;

public class Subject(Guid id, string name)
{
    private Guid Id { get; } = id;
    private string Name { get; } = name;
}