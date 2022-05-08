using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.Model;
using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Product;
using AgriProductTracking.util;
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

        public async Task<ResponseViewModel> UploadProductImage(FileContainerViewModel container)
        {
            var response = new ResponseViewModel();

            try
            {
                var product = _db.Products.FirstOrDefault(x => x.Id == container.Id);

                var folderPath = GetProductImageFolderPath(product, _configuration);
                var firstFile = container.Files.FirstOrDefault();

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                if (firstFile != null && firstFile.Length > 0)
                {
                    var fileName = GetProductImageName(product, Path.GetExtension(firstFile.FileName));
                    var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await firstFile.CopyToAsync(stream);
                       /* var expenseImage = new ExpenseImage()
                        {
                            AttachementName = fileName,
                            Attachment = filePath

                        };*/

                       // expense.ExpenseImages.Add(expenseImage);
                        response.Message = "Product image has been uploaded succesfully";
                    }
                }

                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }

            return response;
        }


        #region Private Methods
        private string GetProductImageFolderPath(Product model, IConfiguration configuration)
        {
            return string.Format(@"{0}{1}\{2}", configuration.GetSection("FileUploadPath").Value, FolderNames.PRODUCT, model.Id);
        }

        public static string GetProductImageName(Product model, string extension)
        {
            return string.Format(@"Product-Image-{0}-{1}{2}", model.Id, Guid.NewGuid(), extension);
        }

        /*public static string GetExpenseImagePath(ExpenseImage model, IConfiguration config, long expenseId)
        {
            return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.EXPENSES, model.ExpenseId, model.AttachementName);

        }*/
        #endregion
    }

   
        
}
