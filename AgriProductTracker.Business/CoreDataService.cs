using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.Model.Common.Enums;
using AgriProductTracker.ViewModel;
using AgriProductTracking.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class CoreDataService : ICoreDataService
    {
        private readonly AgriProductTrackerDbContext _db;

        public CoreDataService(AgriProductTrackerDbContext _db)
        {
            this._db = _db;
        }

        public List<DropDownViewModel> GetDeliveryServices()
        {
            var deliverySservices = _db.DeliveryServices.Where(x => x.IsActive == true)
               .Select(x => new DropDownViewModel()
               {
                   Id = x.Id,
                   Name = x.Name
               }).ToList();

            return deliverySservices;
        }

        public List<DropDownViewModel> GetProductCategories()
        {
            var productCategories = _db.ProductCategories.Where(x => x.IsActive == true)
                .Select(x => new DropDownViewModel() 
                { 
                    Id = x.Id, 
                    Name = x.Name 
                }).ToList();

            return productCategories;
        }
        public List<DropDownViewModel> GetPaymentType()
        {
            var response = new List<DropDownViewModel>();

            foreach (PaymentType expenses in (PaymentType[])Enum.GetValues(typeof(PaymentType)))
            {
                response.Add(new DropDownViewModel() 
                { 
                    Id = (int)expenses, 
                    Name = EnumHelper.GetEnumDescription(expenses) 
                });
            }

            return response;
        }
    }
}
