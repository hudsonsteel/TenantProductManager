using Microsoft.EntityFrameworkCore;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;
using TenantProductManager.Infrastructure.DataBaseContext;

namespace TenantProductManager.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Name == username);
        }
        public async Task<User> GetByUsernameOrEmailAsync(string username, string email)
        {
            return await _context.Users
                .Where(u => u.Name == username || u.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
