using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.RestApi.Infrastructure.Services;
using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AgriProductTracker.RestApi.Controllers
{
    //[Authorize]
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

        [HttpGet]
        [Route("getPrductById/{id:int}")]
        public IActionResult GetPrductById(int id)
        {
            var response = _productService.GetPrductById(id);

            return Ok(response);
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("uploadProductImage")]
        public async Task<IActionResult> UploadProductImage()
        {
            var container = new FileContainerViewModel();

            var request = await Request.ReadFormAsync();

            container.Id = int.Parse(request["id"]);
            container.Type = int.Parse(request["type"]);

            foreach (var file in request.Files)
            {
                container.Files.Add(file);
            }

            var response = await _productService.UploadProductImage(container);

            return Ok(response);
        }

        [HttpPost]
        [Route("getAllProducts")]
        public ActionResult GellAllExpeses(ProductFilterViewModel filter)
        {
            var response = _productService.GellAllProducts(filter);

            return Ok(response);
        }

        [HttpGet]
        [RequestSizeLimit(long.MaxValue)]
        [Route("downloadProductImage/{id:int}")]
        [ProducesResponseType(typeof(DownloadFileViewModel), (int)HttpStatusCode.OK)]
        public FileStreamResult DownloadProductImange(int id)
        {
            var response = _productService.DownloadProductImage(id);

            return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
        }

        [HttpDelete]
        [Route("deleteProductImage/{id:int}")]
        public async Task<ActionResult> DeleteExpenseReceiptImage(int id)
        {
            var response = await _productService.DeleteProductImage(id);

            return Ok(response);
        }
    }
}
