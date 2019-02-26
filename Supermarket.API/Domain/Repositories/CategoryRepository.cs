using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryRepository(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<Category>> ListAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}