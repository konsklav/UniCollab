using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IPostRepository : IUnitOfWork
{
    Task<Post?> GetBySlugAsync(string slug);
    Task<List<Post>> GetMostRecentAsync(int count);
    Task<List<Post>> GetByUserAsync(Guid userId);
    void Add(Post post);
}