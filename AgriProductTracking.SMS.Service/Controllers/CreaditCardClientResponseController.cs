using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Common;
using AgriProductTracker.ViewModel.Order;
using AgriProductTracking.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriProductTracking.SMS.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreaditCardClientResponseController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CreaditCardClientResponseController(IConfiguration _configuration)
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
    }
}
