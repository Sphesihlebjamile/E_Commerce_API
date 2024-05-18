using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        Task<List<Category>?> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid categoryId);
        Task Insert(Category category);
        void Update(Category category);
        Task Delete(Guid categoryId);
        //Task AddProductToCategory(Guid categoryId, Guid productId);
    }
}