namespace Saas.Application.Interfaces.Data;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}