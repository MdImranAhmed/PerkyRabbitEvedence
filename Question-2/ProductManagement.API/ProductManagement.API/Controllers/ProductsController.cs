using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Repository;
using ProductManagement.Library.Domain;
using ProductManagement.Library.Dtos;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductsController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product requestModel)
        {
            try
            {
                var product = await _productRepo.CreateProductAsync(requestModel.Name, requestModel.Price, requestModel.CategoryId, requestModel.SupplierId);
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product requestModel)
        {
            try
            {
                var product = await _productRepo.UpdateProductAsync(id, requestModel.Name, requestModel.Price, requestModel.CategoryId, requestModel.SupplierId);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepo.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepo.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
