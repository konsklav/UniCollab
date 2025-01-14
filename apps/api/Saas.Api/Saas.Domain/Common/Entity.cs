namespace Saas.Domain.Common;

/// <summary>
/// Base class for all entity types
/// </summary>
public abstract class Entity(Guid? id = null)
{
    // Optional 'id' parameter above, we give the developer two choices:
    // 1. Create an entity with a specific ID
    // 2. Let the entity generate the ID itself.

    public Guid Id { get; private set; } = id ?? Guid.CreateVersion7();

    public override bool Equals(object? obj)
    {
        if (obj is not Entity entity)
            return false;

        return entity.Id == Id;
    }
    
    public static bool operator ==(Entity? a, Entity? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;
    
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;
    
        return a.Equals(b);
    }
    
    public static bool operator !=(Entity? a, Entity? b) => !(a == b);

    public override int GetHashCode() => (ToString() + Id).GetHashCode();
}