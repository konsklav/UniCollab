using Saas.Domain.Posts;

namespace Saas.Application.Interfaces.Data;

public interface IPostRepository : IUnitOfWork
{
    Task<Post?> GetBySlugAsync(string slug);
    
    Task<List<Post>> GetMostRecentAsync(int count);
}