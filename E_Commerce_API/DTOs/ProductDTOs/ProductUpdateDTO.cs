namespace E_Commerce_API.DTOs.Product
{
    public class ProductUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid? CategoryId { get; set; }
    }
}