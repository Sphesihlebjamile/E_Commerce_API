namespace E_Commerce_API.Data
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Wishlist>? Wishlist { get; set; }
    }
}
