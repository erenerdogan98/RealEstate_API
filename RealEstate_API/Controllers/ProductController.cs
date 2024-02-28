using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

        [HttpGet("productlist")]
        public async Task<IActionResult> ProductList()
        {
            var products = await _productRepository.GetAllAsync() ?? throw new InvalidOperationException("Products not found");
            return Ok(products);
        }
        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategory() ?? throw new ArgumentNullException("No products found for this category.");
            return Ok(products);
        }

    }
}
