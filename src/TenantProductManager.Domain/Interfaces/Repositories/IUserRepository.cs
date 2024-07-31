using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User> GetByUsernameOrEmailAsync(string username, string email);
    }
}
