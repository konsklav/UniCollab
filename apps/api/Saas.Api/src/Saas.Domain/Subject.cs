using System.Reflection.Metadata.Ecma335;
using Saas.Domain.Common;

namespace Saas.Domain;

public class Subject : Entity
{
    private Subject() {}
    public Subject(string name, Guid? id = null) : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }
}