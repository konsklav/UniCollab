namespace Saas.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}