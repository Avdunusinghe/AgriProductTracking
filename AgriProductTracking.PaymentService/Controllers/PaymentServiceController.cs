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
using AgriProductTracker.Model.Common.Enums;

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
			var customeOrderResponse = new CustomerOrderResponseViewModel();

            try
            {
				string userName = string.Empty;

				userName = _identityService.GetUserName();

				var logggedInUser = _curretUserService.GetUserByUsername(userName);

				var order = new Order()
				{
					TotalPrice = model.Amount,
					CustomerId = logggedInUser.Id,
					DateTime = DateTime.UtcNow,
					IsProceesed = false,
					ShippingAddress = model.ShippingAddress,
					PostalCode = model.PostalCode,
					City = model.City,

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

				if (model.PaymentType == PaymentType.CreditCard)
				{

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
						transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
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
						if (response.transactionResponse != null)
						{
							customeOrderResponse.IsSuccess = true;
							customeOrderResponse.CustomerEmail = logggedInUser.Email;
							customeOrderResponse.CustomerMobileNumber = logggedInUser.MobileNumber;

						}
					}
					else
					{
						if (response.transactionResponse != null)
						{
							customeOrderResponse.IsSuccess = false;
							customeOrderResponse.Message = "Payment Details Error Please try Again";
						}
					}

				}
				else
				{
					customeOrderResponse.IsSuccess = false;
					customeOrderResponse.CustomerEmail = logggedInUser.Email;
					customeOrderResponse.CustomerMobileNumber = logggedInUser.MobileNumber;
				}

			}
			catch (Exception ex)
            {

            }
			
			return Ok(customeOrderResponse);
		}


    }
}
