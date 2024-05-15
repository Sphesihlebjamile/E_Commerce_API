namespace E_Commerce_API.Data
{
    public class ApplicationDbContext
        : DbContext
    {

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(
                    new {
                        Id = Guid.NewGuid(),
                        Name = "D5 Wifi SLR Camera",
                        Price = 1298.00,
                        Description = "The perfect Canon Camera for content creation"
                    },
                    new {
                        Id = Guid.NewGuid(),
                        Name = "Godox AD400PRO",
                        Price = 14495.00,
                        Description = "Godox AD400PRO, All In One Outdoor Flash",
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
