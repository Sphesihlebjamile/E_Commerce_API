
namespace E_Commerce_API.Repositories.Abstract
{
    public interface ICartRepository
    {
        Task<List<Cart>?> GetAllAsync();
        Task<Cart?> GetByIdAsync(Guid id);
        Task<double?> GetTotalPrice(Guid userId);
        Task<List<Cart>?> GetCartItemsForCustomer(Guid userId);
        Task Insert(Cart cart);
        void Update(Cart cart);
        Task Delete(Guid id);
    }
}