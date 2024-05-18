using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Repositories.Concrete
{
    public class CategoryRepository
        : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }

        public async Task Delete(Guid categoryId)
        {
            if(await _dbContext.Categories
                .FirstOrDefaultAsync(cat => cat.Id == categoryId)
                is Category category)
                _dbContext.Categories.Remove(category);
        }

        public async Task<List<Category>?> GetAllAsync()
        {
            var categories = await _dbContext.Categories
                    .Include(cat => cat.Products)
                    .ToListAsync();
            return categories == null || categories.Count == 0 ?
                null : categories;
        }

        public async Task<Category?> GetByIdAsync(Guid categoryId)
        {
            return await _dbContext.Categories
                    .Include(cat => cat.Products)
                    .FirstOrDefaultAsync(cat => cat.Id == categoryId);
        }

        public async Task Insert(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }
    }
}