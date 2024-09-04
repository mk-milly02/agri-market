using System.ComponentModel.DataAnnotations;
using AgriMarket.API.Models.Domain.Auth;

namespace AgriMarket.API.Models.Domain.Products
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string ImagePublicId { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        // Navigation Properties
        public User User { get; set; }
    }
}