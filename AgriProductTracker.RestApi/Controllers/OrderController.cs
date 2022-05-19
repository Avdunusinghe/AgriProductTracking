using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.RestApi.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriProductTracker.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IIdentityService _identityService;

        public OrderController(IOrderService _orderService, IIdentityService _identityService)
        {
            this._orderService = _orderService;
            this._identityService = _identityService;
        }

       /* [HttpGet]
        [Route("{id}")]
        public ActionResult GetOrderById(int id)
        {
            var response = _orderService.GetOrderById(id);

            return Ok(response);
        }*/

        [HttpGet]
        [Route("getAllOrders")]
        public ActionResult GetAllOrders()
        {
            var response = _orderService.GetAllOrders();

            return Ok(response);
        }

        [HttpGet]
        [Route("confirmOrder/{orderId:int}/{deliveryPartnerId:int}")]
        public async Task<ActionResult> ConfirmOrder(int orderId, int deliveryPartnerId)
        {
            var response = await _orderService.ConfirmOrder(orderId, deliveryPartnerId);
            return Ok(response);
        }


        [HttpGet]
        [Route("getOrderById/{id}")]
        public IActionResult GetOrderById(int id)
        {
            var response = _orderService.GetOrderById(id);

            return Ok(response);
        }





    }
}
