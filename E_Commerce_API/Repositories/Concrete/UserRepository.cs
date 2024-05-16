using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.Repositories.Abstract;

namespace E_Commerce_API.Repositories.Concrete
{
    public class UserRepository(ApplicationDbContext dbContext)
                : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async void Delete(Guid id)
        {
            User? user = await GetByIdAsync(id);
            if(user == null)
                return;
            _dbContext.Users.Remove(user);
        }

        public async Task<List<User>?> GetAllAsync()
        {
            return await _dbContext.Users
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync
                        (user => user.Id == id);
        }

        public async Task Insert(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            return await _dbContext.Users
                    .AnyAsync(user => 
                        (user.Email.Equals(username) ||
                        user.Username.Equals(username)) &&
                        user.Password.Equals(password));
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }

        public Task<bool> ValidateEmail(string email)
        {
            return _dbContext.Users
                    .AnyAsync(user => user.Email == email);
        }

        public Task<bool> ValidateUsername(string username)
        {
            return _dbContext.Users
                    .AnyAsync(user => user.Username == username);
        }
    }
}