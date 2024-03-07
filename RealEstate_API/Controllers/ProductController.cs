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

        [HttpGet("ProductWithCategory/{id}")]
        public async Task<IActionResult> GetProductWithCategory(int id)
        {
            var product = await _productRepository.GetProductWithCategoryAsync(id) ?? throw new ArgumentNullException($"No product found for this category with Id : {id}");
            return Ok(product);
        }

        [HttpGet("ChaneDealOfTheDay/{id}")]
        public async Task<IActionResult> ProductDealOfTheStatusChangeAsync(int id)
        {
            var product = await _productRepository.GetProductWithCategoryAsync(id);
            if (product == null)
                return NotFound();

            product.DealOfTheDay = !product.DealOfTheDay;
            await _productRepository.ChangeDealOfStatusAsync(id);

            var statusMessage = product.DealOfTheDay ? "true" : "false";
            return Ok($"Deal of the Day status changed to {statusMessage}.");
        }

        [HttpGet("LastFiveProducts")]
        public async Task<IActionResult> GetLastFiveRentedProductsListAsync()
        {
            var lastFiveProducts = await _productRepository.GetlLastFiveRentedProductsAsync() ?? throw new ArgumentException("Can not get last five products");
            return Ok(lastFiveProducts);
        }

        [HttpGet("ProductAdvertListByEmployee/{id}")]
        public async Task<IActionResult> GetProductAdvertsByEmployeeAsync(int id)
        {
            var productsByEmployee = await _productRepository.GetProductAdvertListByEmployeeAsync(id) ?? throw new ArgumentException($"Can not get  product adverts by Employee with Employee Id : {id}");
            return Ok(productsByEmployee);
        }
    }
}
