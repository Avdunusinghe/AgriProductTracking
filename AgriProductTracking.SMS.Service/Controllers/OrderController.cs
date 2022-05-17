using AgriProductTracking.SMS.Service.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.IpMessaging.V1;

namespace AgriProductTracking.SMS.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        [HttpPost]
        public IActionResult DeliveryServiceOrder(DeliveryServiceOrderViewModel model)
        {
            //string accountSid = Environment.GetEnvironmentVariable("AC88385d7dc77d75393542bc1f8861dd93");
            //string authToken = Environment.GetEnvironmentVariable("62c7c9e367d5db38d36f2c25b2e98fda");

            var accountSid = "AC88385d7dc77d75393542bc1f8861dd93";
            var authToken = "62c7c9e367d5db38d36f2c25b2e98fda";
            


            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
           body: "You have an order. Please collect the order",
          //from: new Twilio.Types.PhoneNumber("+15005550006"),
          from: "+15005550006",
            //messagingServiceSid: "MG9752274e9e519418a7406176694466fa",
            //to: new Twilio.Types.PhoneNumber(model.TelephoneNumber)
            to: model.TelephoneNumber
       ); 

            Console.WriteLine(message.Sid);
            var response = "Success";
            return Ok(response);
            
        }

    }
}
