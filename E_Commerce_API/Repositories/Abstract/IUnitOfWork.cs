
namespace E_Commerce_API.Repositories.Abstract
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IValidationRepsitory ValidationRepsitory { get; set; }
        ICartRepository CartRepository { get; set; }

        Task SaveChangesAsync();
    }
}