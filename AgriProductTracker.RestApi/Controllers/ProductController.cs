using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.RestApi.Infrastructure.Services;
using AgriProductTracker.ViewModel.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriProductTracker.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IIdentityService _identityService;

        public ProductController(IProductService _productService,IIdentityService _identityService)
        {
            this._productService = _productService;
            this._identityService = _identityService;
        }

       [HttpPost]
        public async Task<ActionResult> SaveProduct([FromBody] ProductViewModel vm)
        {
            var userName = _identityService.GetUserName();
            var response = await _productService.ProductSave(vm, userName);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ProductDelete(int id)
        {
            var response = await _productService.ProductDelete(id);

            return Ok(response);
        }

    }
}
