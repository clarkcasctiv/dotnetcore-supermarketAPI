using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email);
        Task AddAsync(User user, ERole[] userRoles);
    }
}