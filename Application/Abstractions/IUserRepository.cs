using Domain.Entities;

namespace Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<(List<User>, int)> GetAllPaginatedAsync(int page, int size);
    }
}
