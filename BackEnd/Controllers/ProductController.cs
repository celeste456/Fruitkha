using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        IImageHandler _imageHandler;

        public ProductController(IProductService productService, IImageHandler imageHandler)
        {
            _productService = productService;
            _imageHandler = imageHandler;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _productService.GetProducts();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            return Ok(result);
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult GetGetProductsByCategory(int categoryId)
        {
            var result = _productService.GetProductsByCategory(categoryId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddProduct([FromForm] ProductDTO productDto, IFormFile? photo)
        {
            try
            {
                var addedProduct = _productService.Add(productDto, photo);
                return Ok("Product added successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Error processing the image: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }

        [HttpPut]
        public ActionResult<ProductDTO> Put([FromForm] ProductDTO productDto, IFormFile? photo)
        {
            var updatedProduct = _productService.Update(productDto, photo);
            return Ok(updatedProduct); 
        }

    }
}
