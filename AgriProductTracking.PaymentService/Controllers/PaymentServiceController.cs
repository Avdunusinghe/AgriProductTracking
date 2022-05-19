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
		private readonly IPaymentService _paymentService;
		private readonly AgriProductTrackerDbContext _db;

		public PaymentServiceController
		(
			IConfiguration _configuration, 
			ICurrentUserService _curretUserService,
			IIdentityService _identityService,
			IPaymentService _paymentService,
		    AgriProductTrackerDbContext _db
		)
        {
			this._configuration = _configuration;
			this._curretUserService = _curretUserService;
			this._identityService = _identityService;
			this._paymentService = _paymentService;
			this._db = _db;
        }
       

        [HttpPost]
		public async Task<IActionResult> Payment(OrderContainerViewModel model)
        {
			
			var userName = _identityService.GetUserName();

			var response = await _paymentService.Payment(model, userName);
			
			return Ok(response);
		}


    }
}
