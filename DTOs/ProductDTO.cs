using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LemonHiveEcommerce.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Qty { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }

}
