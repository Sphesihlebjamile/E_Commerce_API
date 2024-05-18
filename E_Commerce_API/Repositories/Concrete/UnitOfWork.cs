
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
            IValidationRepsitory validationRepsitory,
            ICartRepository cartRepository,
            ICategoryRepository categoryRepository
        )
        {
            _dbContext = dbContext;
            ProductRepository = productRepository;
            UserRepository = userRepository;
            ValidationRepsitory = validationRepsitory;
            CartRepository = cartRepository;
            CategoryRepository = categoryRepository;
        }

        public IProductRepository ProductRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IValidationRepsitory ValidationRepsitory { get; set; }
        public ICartRepository CartRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}