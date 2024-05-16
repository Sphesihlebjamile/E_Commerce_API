using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Repositories.Concrete
{
    public class CartRepository
        : ICartRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public CartRepository(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }

        public async Task Delete(Guid id)
        {
            Cart? cart = await _dbContext.Carts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if(cart == null)
                return;
            cart.IsDeleted = true;
            Update(cart);
        }

        public async Task<List<Cart>?> GetAllAsync()
        {
            var carts = await _dbContext.Carts
                    .AsNoTracking()
                    .Include(cart => cart.Product)
                    .Include(cart => cart.User)
                    .Where(cart => !cart.IsDeleted)
                    .ToListAsync();
            return carts == null || carts.Count == 0 ?
                    null : carts;
        }

        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Carts
                    .AsNoTracking()
                    .Include(cart => cart.Product)
                    .FirstOrDefaultAsync(
                        cart => cart.Id == id &&
                                !cart.IsDeleted
                    );
        }

        public async Task<List<Cart>?> GetCartItemsForCustomer(Guid userId)
        {
            return (await GetAllAsync())?
                    .Where(items => items.UserId == userId)
                    .ToList();
        }

        public async Task<double?> GetTotalPrice(Guid userId)
        {
            double totalPrice = 0D;
            if(await GetCartItemsForCustomer(userId: userId)
                is not List<Cart> userCartItems)
                return null;
            
            userCartItems.ForEach(cartItem => {
                totalPrice += (cartItem.Product!.Price * cartItem.Quantity);
            });
            return totalPrice;
        }

        public async Task Insert(Cart cart)
        {
            await _dbContext.Carts.AddAsync(cart);
        }

        public void Update(Cart cart)
        {
            _dbContext.Carts.Update(cart);
        }
    }
}