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
using AgriProductTracker.Data.Data;
using AgriProductTracker.Model;
using AgriProductTracking.PaymentService.Infrastructure.Services;

namespace AgriProductTracking.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServiceController : ControllerBase
    {
		private readonly IConfiguration _configuration;
		private readonly ICurrentUserService _curretUserService;
		private readonly IIdentityService _identityService;
		private readonly AgriProductTrackerDbContext _db;

		public PaymentServiceController
		(
			IConfiguration _configuration, 
			ICurrentUserService _curretUserService,
			IIdentityService _identityService,
		    AgriProductTrackerDbContext _db
		)
        {
			this._configuration = _configuration;
			this._curretUserService = _curretUserService;
			this._identityService = _identityService;
			this._db = _db;
        }
       

        [HttpPost]
		public async Task<IActionResult> Payment(OrderContainerViewModel model)
        {
			string userName = string.Empty;

			userName = _identityService.GetUserName();

			var logggedInUser = _curretUserService.GetUserByUsername(userName);

			var order = new Order()
			{
				TotalPrice = model.Amount,
				CustomerId = logggedInUser.Id,
				DateTime = DateTime.UtcNow,
				IsProceesed = false
			};

			order.OrderItems = new HashSet<OrderItem>();

			foreach (var item in model.OrderItems)
			{
				var productItem = new OrderItem()
				{
					OrderId = order.Id,
					ProductId = item.Id,
					NumberOfItems = item.Quantity,

				};

				order.OrderItems.Add(productItem);
			}

			_db.Orders.Add(order);

			await _db.SaveChangesAsync();
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
