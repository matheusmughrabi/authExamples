using DemoStore.Identity.Constants;
using DemoStore.WebApi.Attributes;
using DemoStore.WebApi.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoStore.WebApi.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly ProductRepositoryMock _productRepository;

        public ProductController(ProductRepositoryMock productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("get")]
        [Authorize()]
        public async Task<IActionResult> Get()
        {
            return Ok(_productRepository.GetProducts());
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = Policies.BusinessHours, Roles = Roles.Admin)]
        [ClaimAuthorize(Claims.Product.Type, Claims.Product.Delete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            _productRepository.DeleteProductById(id);
            return Ok();
        }
    }
}
