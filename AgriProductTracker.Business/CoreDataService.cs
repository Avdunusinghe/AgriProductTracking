using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.ViewModel;
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
    }
}
