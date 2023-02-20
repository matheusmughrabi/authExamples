using DemoClientServer.WebApi.Constants;
using DemoClientServer.WebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoClientServer.WebApi.Controllers;

public class CatalogController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ProductRepository _productRepository;

    public CatalogController(IAuthorizationService authorizationService, ProductRepository productRepository)
    {
        _authorizationService = authorizationService;
        _productRepository = productRepository;
    }

    [HttpPost("Catalog/Create")]
    [Authorize(Policy = PolicyConstants.ProductCreate)]
    public async Task<IActionResult> Create()
    {
        return Ok("Product created");
    }

    [HttpPut("Catalog/Update")]
    public async Task<IActionResult> Update(int id)
    {
        var product = _productRepository.GetProductById(id);
        if (product is null)
            return NotFound();

        var isAuthorized = await _authorizationService.AuthorizeAsync(User, product, PolicyConstants.ProductOwnerOnly);
        if (isAuthorized.Succeeded)
            return Ok("Product updated");

        return Forbid();
    }
}
