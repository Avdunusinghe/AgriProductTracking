using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.ViewModel.Order;
using Castle.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class OrderService : IOrderService
    {
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public OrderService(AgriProductTrackerDbContext _db, IConfiguration _configuration, ICurrentUserService _currentUserService)
        {
            this._db = _db;
            this._configuration = _configuration;
            this._currentUserService = _currentUserService;
        }
        public List<OrderContainerViewModel> GetAllOrders()
        {
            var dataSet = new List<OrderContainerViewModel>();

            try
            {
                var query = _db.Orders.Where(o => o.IsProceesed == false).OrderBy(d=>d.DateTime);

                var userList = query.ToList();

                foreach (var item in userList)
                {
                    var vm = new OrderContainerViewModel();

                    vm.Id = item.Id;
                    vm.Amount = item.TotalPrice;

                    
                }


            }catch (Exception ex)
            {

            }

            return dataSet;
        }
    }
}
