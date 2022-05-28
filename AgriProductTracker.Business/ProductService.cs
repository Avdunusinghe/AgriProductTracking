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
        #region Private Members
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructor
        public ProductService
        (
            AgriProductTrackerDbContext _db, 
            IConfiguration _configuration, 
            ICurrentUserService _currentUserService
        )
        {
            this._db = _db;
            this._configuration = _configuration;
            this._currentUserService = _currentUserService;
        } 
        #endregion

        #region Business Services Methods
        public async Task<ResponseViewModel> ProductDelete(long id)
        {
            var response = new ResponseViewModel();

            try
            {
                var product = _db.Products.FirstOrDefault(x => x.Id == id && x.IsActive == true);

                if (product != null)
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

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
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
                    product = new Product()
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        Price = vm.Price,
                        CategoryId = vm.CategoryId,
                        Quantity = vm.Quantity,
                        IsActive = true,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedById = loggedInUser.Id,
                        CreatedOn = DateTime.UtcNow,
                        CreatedById = loggedInUser.Id,

                    };
              
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
                        var productImage = new ProductImage()
                        {
                            AttachementName = fileName,
                            Attachment = filePath

                        };

                        product.ProductImages.Add(productImage);

                        response.IsSuccess = true;
                        response.Message = "Product image has been uploaded succesfully";
                    }
                }

                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Product image Upload Faild,Please try again";
            }

            return response;
        }
        public ProductViewModel GetPrductById(int id)
        {
            var response = new ProductViewModel();
            try
            {
                var query = _db.Products.Where(x => x.Id == id).FirstOrDefault();

                response.Id = query.Id;
                response.Name = query.Name;
                response.Description = query.Description;
                response.CategoryId = query.CategoryId;
                response.Price = query.Price;
                response.Quantity = query.Quantity;

                var  productImages = query.ProductImages.ToList();

                foreach (var item in productImages)
                {
                    if (!string.IsNullOrEmpty(item.AttachementName))
                    {
                        var productImage = string.Format(@"{0}{1}\{2}\{3}", _configuration.GetSection("FileUploadPath").Value, FolderNames.PRODUCT, query.Id, item.AttachementName);

                        if (File.Exists(productImage))
                        {
                            response.ProductImages.Add(new ProductImageViewModel()
                            {
                                Id = item.Id,
                                AttachmentName = item.Attachment,
                                Attachment = "data:image/jpg;base64," + ImageHelper.getThumnialImage(productImage),
                            });
                        }
                    }
                }
                


            }catch (Exception ex)
            {
                
            }

            return response;
            
        }
        public PaginatedItemsViewModel<ProductViewModel> GellAllProducts(ProductFilterViewModel filter)
        {
            int totalRecordCount = 0;
            int totalPageCount = 0;
            
            var data = new List<ProductViewModel>();

            var query = _db.Products.Where(x=>x.IsActive == true);

            if(filter.CaregoryId > 0)
            {
               query = query.Where(x=>x.CategoryId == filter.CaregoryId).OrderBy(x=>x.CreatedOn);
            }

            if(!string.IsNullOrEmpty(filter.SearchText))
            {
                query = query.Where(x => x.Name.Contains(filter.SearchText)).OrderBy(n => n.Name);
            }

            totalRecordCount = query.Count();

            totalPageCount = (int)Math.Ceiling((Convert.ToDecimal(totalRecordCount) / filter.PageSize));

            var pageData = query.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            foreach (var item in pageData)
            {
                var product = new ProductViewModel();

                product.Id = item.Id;
                product.Name = item.Name;
                product.Description = item.Description;
                product.CategoryId = item.CategoryId;
                product.Quantity = item.Quantity;
                product.Price = item.Price;
                product.CreatedByName = item.CreatedBy.FullName;
                product.UpdatedByName = item.UpdatedBy.FullName;

                var productImages = item.ProductImages.ToList();

                foreach(var image in productImages)
                {
                    if (!string.IsNullOrEmpty(image.AttachementName))
                    {
                       var productImage = string.Format(@"{0}{1}\{2}\{3}", _configuration.GetSection("FileUploadPath").Value, FolderNames.PRODUCT, item.Id, image.AttachementName);

                         if (File.Exists(productImage))
                         {
                             product.ProductImages.Add(new ProductImageViewModel()
                             {
                                  Id = image.Id,
                                  AttachmentName = image.AttachementName,
                                  Attachment = "data:image/jpg;base64," + ImageHelper.getThumnialImage(productImage),
                             });
                         }
                     }
                }
                    
                  data.Add(product);
            }

             var productDataSet = new PaginatedItemsViewModel<ProductViewModel>(filter.CurrentPage, filter.PageSize, totalPageCount, totalRecordCount, data);

             return productDataSet;
          
        }
        public DownloadFileViewModel DownloadProductImage(int id)
        {
            var response = new DownloadFileViewModel();

            try
            {
                var productImage = _db.ProductImages.Where(x => x.Id == id).FirstOrDefault();

                var imagePath = GetProductImagePath(productImage, _configuration, productImage.ProductId);

                response.FileName = productImage.AttachementName;

                byte[] fileContents = null;
                MemoryStream memoryStream = new MemoryStream();

                using (FileStream fileStream = File.OpenRead(imagePath))
                {
                    fileStream.CopyTo(memoryStream);
                    fileContents = memoryStream.ToArray();
                    memoryStream.Dispose();
                    response.FileData = fileContents;

                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }
        public async Task<ResponseViewModel> DeleteProductImage(int id)
        {
            var response = new ResponseViewModel();

            try
            {
                var image = _db.ProductImages.Where(x => x.Id == id).FirstOrDefault();

                if (File.Exists(image.Attachment))
                {
                    File.Delete(image.Attachment);
                    _db.ProductImages.Remove(image);
                    await _db.SaveChangesAsync();
                }

                response.IsSuccess = true;
                response.Message = "Product image has been deleted";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = " Error has been orccured,Please try again";
            }

            return response;
        }


        #endregion

        #region Private Methods
        private string GetProductImageFolderPath(Product model, IConfiguration configuration)
        {
            return string.Format(@"{0}{1}\{2}", configuration.GetSection("FileUploadPath").Value, FolderNames.PRODUCT, model.Id);
        }

        public static string GetProductImageName(Product model, string extension)
        {
            return string.Format(@"Product-Image-{0}-{1}{2}", model.Id, Guid.NewGuid(), extension);
        }

        public static string GetProductImagePath(ProductImage model, IConfiguration configuration, long expenseId)
        {
            return string.Format(@"{0}{1}\{2}\{3}", configuration.GetSection("FileUploadPath").Value, FolderNames.PRODUCT, model.ProductId, model.AttachementName);

        }

       



        #endregion
    }

   
        
}
