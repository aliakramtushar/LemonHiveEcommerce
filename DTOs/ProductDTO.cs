namespace LemonHiveEcommerce.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Qty { get; set; }
    }

}
