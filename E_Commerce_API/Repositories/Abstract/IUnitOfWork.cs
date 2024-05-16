using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Repositories.Abstract
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IValidationRepsitory ValidationRepsitory { get; set; }

        Task SaveChangesAsync();
    }
}