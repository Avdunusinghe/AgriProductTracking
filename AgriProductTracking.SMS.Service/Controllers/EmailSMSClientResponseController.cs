using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Common;
using AgriProductTracker.ViewModel.Order;
using AgriProductTracking.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AgriProductTracking.SMS.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSMSClientResponseController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmailSMSClientResponseController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        [HttpPost]
        public IActionResult SendPaymentSuccessMesseage(CustomerOrderResponseViewModel model)
        {
            var response = new ResponseViewModel();

            try
            {
                var _companyEmailSettings = new CompanyEmailSettingModel()
                {
                    SMTPServer = _configuration["SMTPServer"],
                    SMTPUsername = _configuration["SMTPUsername"],
                    SMTPFrom = _configuration["SMTPFrom"],
                    SMTPPassword = _configuration["SMTPPassword"],
                    SMTPPort = 587,
                    IsSMTPUseSSL = true,
                    IsEnableHTML = true,

                };

                bool isSend = EmailHelper.SendPaymentSuccessEmail(_companyEmailSettings, model);

                if (isSend)
                {
                    var accountSid = _configuration["TWILIO_ACCOUNT_SID"];
                    var authToken = _configuration["TWILIO_AUTH_TOKEN"];

                    var temoporyNumber = model.CustomerMobileNumber.Remove(0, 1);
                   
                    var mobileNumber = string.Format("{0}{1}", "+94", temoporyNumber); 

                    TwilioClient.Init(accountSid, authToken);

                    var message = MessageResource.Create
                    (
                        body: "Order Success",
                        from: new Twilio.Types.PhoneNumber(_configuration["FromMobileNumber"]),
              
                        to: mobileNumber
                    );

                    response.IsSuccess = true;
                    response.Message = "Payment Sucessfull!..";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error";
            }

            return Ok(response);

        }

        [HttpPost]
        [Route("sendMobilePaymentSuccessMessage")]
        public IActionResult SendMobilePaymentSuccessMessage(CustomerOrderResponseViewModel model)
        {
            var response = new ResponseViewModel();

            try
            {
                var _companyEmailSettings = new CompanyEmailSettingModel()
                {
                    SMTPServer = _configuration["SMTPServer"],
                    SMTPUsername = _configuration["SMTPUsername"],
                    SMTPFrom = _configuration["SMTPFrom"],
                    SMTPPassword = _configuration["SMTPPassword"],
                    SMTPPort = 587,
                    IsSMTPUseSSL = true,
                    IsEnableHTML = true,

                };

                bool isSend = EmailHelper.SendMobileBillAddPaymentMeesage(_companyEmailSettings, model);

                if (isSend)
                {
                    var accountSid = _configuration["TWILIO_ACCOUNT_SID"];
                    var authToken = _configuration["TWILIO_AUTH_TOKEN"];

                    var temoporyNumber = model.CustomerMobileNumber.Remove(0, 1);

                    var mobileNumber = string.Format("{0}{1}", "+94", temoporyNumber);

                    TwilioClient.Init(accountSid, authToken);

                    var message = MessageResource.Create
                    (
                        body: "Add Mobile Bill",
                        from: new Twilio.Types.PhoneNumber(_configuration["FromMobileNumber"]),

                        to: temoporyNumber
                    );
                    response.IsSuccess = true;
                    response.Message = "Payment SucessFull!..";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error";
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("sendDeliveryPatnerMessage")]
        public IActionResult SendDeliveryPatnerMessage(OrderConfirmResponseViewModel model)
        {
            var response = new ResponseViewModel();

            try
            {
                var _companyEmailSettings = new CompanyEmailSettingModel()
                {
                    SMTPServer = _configuration["SMTPServer"],
                    SMTPUsername = _configuration["SMTPUsername"],
                    SMTPFrom = _configuration["SMTPFrom"],
                    SMTPPassword = _configuration["SMTPPassword"],
                    SMTPPort = 587,
                    IsSMTPUseSSL = true,
                    IsEnableHTML = true,

                };

                bool isSend = EmailHelper.SendDeliveryPatnerMessage(_companyEmailSettings, model);

                if (isSend)
                {
                    var accountSid = _configuration["TWILIO_ACCOUNT_SID"];
                    var authToken = _configuration["TWILIO_AUTH_TOKEN"];

                    var temoporyNumber = model.DeliveryServicePhoneNumber.Remove(0, 1);

                    var mobileNumber = string.Format("{0}{1}", "+94", temoporyNumber);


                    TwilioClient.Init(accountSid, authToken);

                    var message = MessageResource.Create
                    (
                        body: "You have an order. Please collect the order",
                        from: new Twilio.Types.PhoneNumber(_configuration["FromMobileNumber"]),

                        to: mobileNumber
                    );
                    response.IsSuccess = true;
                    response.Message = "Order Confirmed..";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error";
            }
            return Ok(response);
        }
    }
}
