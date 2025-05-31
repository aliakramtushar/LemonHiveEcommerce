using System.ComponentModel.DataAnnotations;

namespace LemonHiveEcommerce.DTOs
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public double TotalPrice { get; set; }
        public string ImagePath {  get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
