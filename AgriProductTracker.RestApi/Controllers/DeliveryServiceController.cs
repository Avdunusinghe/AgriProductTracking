using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.RestApi.Infrastructure.Services;
using AgriProductTracker.ViewModel.DeliveryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriProductTracker.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryServiceController : ControllerBase
    {
        private readonly IDeliveryServiceService _deliveryService;
        private readonly IIdentityService _identityService;

        public DeliveryServiceController(IDeliveryServiceService _deliveryService, IIdentityService _identityService)
        {
            this._deliveryService = _deliveryService;
            this._identityService = _identityService;
        }

        [HttpGet]
        [Route("getAllDeliveryServiceList")]

        public ActionResult GetAllDeliveryServiceList()
        {
            var response = _deliveryService.GetAllDeliveryServiceList();
            return Ok(response);
        }

        [HttpPost]
        [Route("deliveryServiceSave")]

        public async Task<ActionResult> Post([FromBody] DeliveryServiceViewModel vm)
        {
            var userName = _identityService.GetUserName();
            var response = await _deliveryService.DeliveryServiceSave(vm, userName);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Route("deliveryServiceDelete")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _deliveryService.DeliveryServiceDelete(id);

            return Ok(response);
        }


        [HttpGet]
        [Route("getAllDeliveryServices")]
        public ActionResult GetAllDeliveryServices()
        {
            var response = _deliveryService.GetAllDeliveryServices();

            return Ok(response);
        }

    }
}
 
