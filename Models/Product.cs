// Models/Product.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LemonHiveEcommerce.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Qty { get; set; }

        public string? ImagePath { get; set; }

        //public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
