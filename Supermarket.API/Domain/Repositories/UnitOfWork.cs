using System.Threading.Tasks;
using Supermarket.API.Domain.Persistence.Contexts;

namespace Supermarket.API.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();

        }
    }
}