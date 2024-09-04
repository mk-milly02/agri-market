using System.Security.Claims;
using AgriMarket.API.Models.Domain.Products;
using AgriMarket.API.Models.DTO.Products.Requests;
using AgriMarket.API.Repositories.Products.Interfaces;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriMarket.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly Cloudinary _cloudinary;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository productRepository, Cloudinary cloudinary, IMapper mapper)
        {
            this.productRepository = productRepository;
            _cloudinary = cloudinary;
            this.mapper = mapper;
        }

        [HttpPost]
        // [Authorize(Roles = "User,SuperUser")]
        public async Task<IActionResult> Create([FromForm] CreateProductDTO createProductDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Console.WriteLine("USER ID = {0}", userId);

            if (userId == null)
            {
                return Unauthorized();
            }

            var uploadResult = new ImageUploadResult();

            if (createProductDTO.Image != null && createProductDTO.Image.Length > 0)
            {
                using var stream = createProductDTO.Image.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(createProductDTO.Image.FileName, stream),
                    Transformation = new Transformation().Crop("fill").Gravity("face").Width(500).Height(500)
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            if (uploadResult.Error != null)
            {
                return BadRequest(new { message = uploadResult.Error.Message });
            }

            var prod = mapper.Map<Product>(createProductDTO);

            prod.UserId = userId;
            prod.ImageUrl = uploadResult.Url.ToString();
            prod.ImagePublicId = uploadResult.PublicId;

            await productRepository.CreateAsync(prod);
            return Ok();
        }
    }
}