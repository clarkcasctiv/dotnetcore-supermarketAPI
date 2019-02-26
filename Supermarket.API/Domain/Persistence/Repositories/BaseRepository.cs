using Supermarket.API.Domain.Persistence.Contexts;

namespace Supermarket.API.Domain.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;

        }
    }
}