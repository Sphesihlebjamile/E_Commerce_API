using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<List<Product>?> GetAllAsync();
        Task<Product?> GetById(Guid prodId);
        Task Insert(Product product);
        void Update(Product product);
        Task Delete(Guid prodId);
    }
}