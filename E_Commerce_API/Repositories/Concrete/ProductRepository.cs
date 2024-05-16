
namespace E_Commerce_API.Repositories.Concrete
{
    public class ProductRepository
        : IProductRepository
    {

        private ApplicationDbContext _dbContext;

        public ProductRepository(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<Product>?> GetAllAsync()
        {
            return await _dbContext.Products
                    .AsNoTracking()
                    .Include(prod => prod.Category)
                    .ToListAsync();
        }

        public async Task<Product?> GetById(Guid prodId)
        {
            return await _dbContext.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync
                        (prod => prod.Id == prodId);
        }

        public async Task Insert(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _dbContext.Products
                    .Update(product);
        }

        public async Task Delete(Guid prodId)
        {
            Product? product = await GetById(prodId);
            if(product == null)
                return;
            _dbContext.Products.Remove(product);
        }

    }
}