using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.Model;
using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Product;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class ProductService : IProductService
    {
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public ProductService(AgriProductTrackerDbContext _db, IConfiguration _configuration, ICurrentUserService _currentUserService)
        {
            this._db = _db;
            this._configuration = _configuration;
            this._currentUserService = _currentUserService;
        }

        public async Task<ResponseViewModel> ProductDelete(long id)
        {
            var response = new ResponseViewModel();

            try
            {
                var product = _db.Products.FirstOrDefault(x => x.Id == id && x.IsActive == true);

                if(product != null)
                {
                    product.IsActive = false;

                    _db.Products.Update(product);

                    response.IsSuccess = true;
                    response.Message = "Product Delete Successfull";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "product Not Found";
                }

                await _db.SaveChangesAsync();

            }catch (Exception ex)
            {
                response.IsSuccess=false;
                response.Message = ex.Message;

            }


            return response;
        }

        public async Task<ResponseViewModel> ProductSave(ProductViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            var loggedInUser = _currentUserService.GetUserByUsername(userName);
            try
            {
                
                var product = _db.Products.FirstOrDefault(p => p.Id == vm.Id);
  

                if (product == null)
                {
                    product.Name = vm.Name;
                    product.Description = vm.Description;
                    product.Price = vm.Price;
                    product.CategoryId = vm.CategoryId;
                    product.Quantity = vm.Quantity;
                    product.IsActive = true;
                    product.UpdatedOn = DateTime.UtcNow;
                    product.UpdatedById = loggedInUser.Id;
                    product.CreatedOn = DateTime.UtcNow;
                    product.CreatedById = loggedInUser.Id;
                    product = _db.Products.FirstOrDefault(p => p.Id == vm.Id);

                    _db.Products.Add(product);

                    response.IsSuccess = true;
                    response.Message = "Product has been save Successfully.";

                }
                else
                {
                    product.Name = vm.Name;
                    product.Description = vm.Description;
                    product.Price = vm.Price;
                    product.CategoryId = vm.CategoryId;
                    product.Quantity = vm.Quantity;
                    product.UpdatedById = loggedInUser.Id;
                    product.UpdatedOn = DateTime.UtcNow;
                    _db.Products.Update(product);

                    response.IsSuccess = true;
                    response.Message = "Product has been update Successfully";

                }

               await _db.SaveChangesAsync();
                                                                                                                                                                                                                                     
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }

   
        
}
