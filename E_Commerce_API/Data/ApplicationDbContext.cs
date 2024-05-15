namespace E_Commerce_API.Data
{
    public class ApplicationDbContext
        : DbContext
    {

        public ApplicationDbContext(
            DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
    }
}
