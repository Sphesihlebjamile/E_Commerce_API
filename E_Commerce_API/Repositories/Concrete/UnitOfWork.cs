using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.Repositories.Abstract;

namespace E_Commerce_API.Repositories.Concrete
{
    public class UnitOfWork
        : IUnitOfWork
    {

        private ApplicationDbContext _dbContext;

        public UnitOfWork(
            ApplicationDbContext dbContext,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IValidationRepsitory validationRepsitory
        )
        {
            _dbContext = dbContext;
            ProductRepository = productRepository;
            UserRepository = userRepository;
            ValidationRepsitory = validationRepsitory;
        }

        public IProductRepository ProductRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IValidationRepsitory ValidationRepsitory { get; set; }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}