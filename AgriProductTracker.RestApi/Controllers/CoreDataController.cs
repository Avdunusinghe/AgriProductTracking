using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.RestApi.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriProductTracker.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreDataController : ControllerBase
    {
        private readonly ICoreDataService _coreDataService;
        private readonly IIdentityService _identityService;

        public CoreDataController(ICoreDataService _coreDataService, IIdentityService _identityService)
        {
            this._coreDataService = _coreDataService;
            this._identityService = _identityService;
        }

        [HttpGet]
        [Route("getAllProductCatagories")]
        public IActionResult GetAllPeoductCategories()
        {
            var response = _coreDataService.GetProductCategories();

            return Ok(response);
        }
    }
}
