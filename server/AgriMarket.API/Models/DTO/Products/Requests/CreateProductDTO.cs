using System.ComponentModel.DataAnnotations;

namespace AgriMarket.API.Models.DTO.Products.Requests
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;
    }
}