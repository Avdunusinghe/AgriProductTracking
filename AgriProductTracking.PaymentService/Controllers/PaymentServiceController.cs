using AgriProductTracking.PaymentService.ViewModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using Microsoft.AspNetCore.Mvc;
using AuthorizeNet.Api.Controllers.Bases;
using AgriProductTracker.ViewModel.Order;
using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.Model;

namespace AgriProductTracking.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServiceController : ControllerBase
    {
		private readonly IConfiguration _configuration;
		private readonly ICurrentUserService _curretUserService;
		//private readonly I
		private readonly AgriProductTrackerDbContext _db;

		public PaymentServiceController(
			IConfiguration _configuration, 
			ICurrentUserService _curretUserService, 
			AgriProductTrackerDbContext _db)
        {
			this._configuration = _configuration;
			this._curretUserService = _curretUserService;
			this._db = _db;
        }
       

        [HttpPost]
        public IActionResult Payment(OrderContainerViewModel model)
        {
			Console.WriteLine("Charge Credit Card Sample");

			ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

			// define the merchant information (authentication / transaction id)
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
			{
				name = _configuration["LoginId"],
				ItemElementName = ItemChoiceType.transactionKey,
				Item = _configuration["Transactionkey"]
			};

			var creditCard = new creditCardType
			{
				cardNumber = model.CardNumber,
				expirationDate = model.ExperationDate,
				cardCode = model.Cvv
			};

			//standard api call to retrieve response
			var paymentType = new paymentType { Item = creditCard };

			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
				amount = model.Amount,
				payment = paymentType
			};

			var request = new createTransactionRequest { transactionRequest = transactionRequest };

			// instantiate the contoller that will call the service
			var controller = new createTransactionController(request);
			controller.Execute();

			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();

			if (response.messages.resultCode == messageTypeEnum.Ok)
			{
				if(response.transactionResponse != null)
				{

					Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);

					string userName = string.Empty;

					

					
				}
			}
			else
			{
				Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
				if (response.transactionResponse != null)
				{
					Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
				}
			}
			return Ok(response);
		}
    }
}
