using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<List<User>?> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task Insert(User user);
        void Update(User user);
        void Delete(Guid id);
        Task<bool> LoginAsync(string username, string password);

        Task<bool> ValidateEmail(string email);
        Task<bool> ValidateUsername(string username);
    }
}