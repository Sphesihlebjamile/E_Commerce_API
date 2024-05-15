namespace E_Commerce_API.Data
{
    public class Wishlist
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Product")]
        public Guid? Product_Id { get; set; }
        public Product? Product { get; set; }
    }
}
