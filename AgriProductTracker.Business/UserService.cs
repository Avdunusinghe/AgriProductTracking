using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.Model;
using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.User;
using AgriProductTracking.util;
using AgriProductTracking.util.Constants.ServiceClassConstants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class UserService : IUserService
    {
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;


        public UserService(AgriProductTrackerDbContext _db, IConfiguration _configuration, ICurrentUserService _currentUserService)
        {
            this._db = _db;
            this._configuration = _configuration;
            this._currentUserService = _currentUserService;
        }

        // Save User Service
        public async Task<ResponseViewModel> SaveUser(UserViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var loggedInUser = _currentUserService.GetUserByUsername(userName);

                var user = _db.Users.FirstOrDefault(x => x.Id == vm.Id);

                var getUserRoles = _db.Roles.FirstOrDefault(x => x.IsActive == true);


                if (user == null)
                {
                    var exisistingUserName = _db.Users.FirstOrDefault(u => u.UserName.Trim().ToUpper() == vm.UserName.Trim().ToUpper());

                    if (exisistingUserName != null)
                    {
                        response.IsSuccess = false;
                        response.Message = UserServiceConstants.EXISTING_USERNAME_ALLREADY_TAKEN_EXCEPTION_MESSAGE;

                        return response;
                    }

                    var exsitingEmail = _db.Users.FirstOrDefault(u => u.Email.Trim().ToUpper() == vm.Email.Trim().ToUpper());

                    if (exsitingEmail != null)
                    {
                        response.IsSuccess = false;
                        response.Message = UserServiceConstants.EXISTING_EMAIL_ALLREADY_TAKEN_EXCEPTION_MESSAGE;

                        return response;
                    }

                    user = new User()
                    {
                        Id = vm.Id,
                        FullName = vm.FullName,
                        Email = vm.Email,
                        MobileNumber = vm.MobileNumber,
                        UserName = vm.UserName,
                        Address = vm.Address,
                        Password = vm.Password,
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        CreatedById = loggedInUser.Id,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedById = loggedInUser.Id
                    };

                    user.UserRoles = new HashSet<UserRole>();

                    foreach (var item in vm.Roles)
                    {
                        var userRole = new UserRole()
                        {
                            RoleId = item,
                            IsActive = true,
                            CreatedById = loggedInUser.Id,
                            CreatedOn = DateTime.UtcNow,
                            UpdatedById = loggedInUser.Id,
                            UpdatedOn = DateTime.UtcNow
                        };

                        user.UserRoles.Add(userRole);
                    }

                    _db.Users.Add(user);

                    // EmailHelper.SendRegisterted(vm.Email, vm.UserName, vm.Password);
                    response.IsSuccess = true;
                    response.Message = UserServiceConstants.NEW_USER_SAVE_SUCCESS_MESSAGE;
                }
                else
                {
                    user.Address = vm.Address;
                    user.FullName = vm.FullName;
                    user.Email = vm.Email;
                    user.MobileNumber = vm.MobileNumber;
                    user.UpdatedById = loggedInUser.Id;
                    user.UpdatedOn = DateTime.UtcNow;

                    var existingRoles = user.UserRoles.ToList();
                    var selectedRols = vm.Roles.ToList();

                    var newRoles = (from r in vm.Roles where !existingRoles.Any(x => x.RoleId == r) select r).ToList();

                    var deletedRoles = (from r in existingRoles where !vm.Roles.Any(x => x == r.RoleId) select r).ToList();

                    foreach (var item in newRoles)
                    {
                        var userRole = new UserRole()
                        {
                            RoleId = item,
                            IsActive = true,
                            CreatedById = loggedInUser.Id,
                            CreatedOn = DateTime.UtcNow,
                            UpdatedById = loggedInUser.Id,
                            UpdatedOn = DateTime.UtcNow
                        };

                        user.UserRoles.Add(userRole);
                    }

                    foreach (var deletedRole in deletedRoles)
                    {
                        user.UserRoles.Remove(deletedRole);
                    }

                    _db.Users.Update(user);

                    response.IsSuccess = true;
                    response.Message = UserServiceConstants.EXISTING_USER_SAVE_SUCCESS_MESSAGE;

                }

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = UserServiceConstants.USER_SAVE_EXCEPTION_MESSAGE;
            }
            return response;
        }

        

       

        public async Task<ResponseViewModel> DeleteUser(long id)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == id && x.IsActive == true);

                if (user != null)
                {
                    user.IsActive = false;

                    _db.Users.Update(user);

                    response.IsSuccess = true;
                    response.Message = "Delete User   Successfull";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "User Not Found";
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
        //GetUserById

        public UserViewModel GetUserbyId(int id)
        {
            var response = new UserViewModel();
           
            var query = _db.Users.FirstOrDefault(x => x.Id == id);

                response.Id = query.Id;
                response.FullName = query.FullName;
                response.UserName = query.UserName;
                response.Address = query.Address;
                response.Email = query.Email;
                response.MobileNumber = query.MobileNumber;
                response.IsActive = query.IsActive;

            var assignedRoles = query.UserRoles.Where(x => x.IsActive == true);

                foreach (var item in assignedRoles)
                {
                    response.Roles.Add(item.RoleId);
                }
            return response;
        }
    



        //Get All roles

        public List<DropDownViewModel> GetAllRoles()
        {
            return _db.Roles.Where(x => x.IsActive == true).Select(r => new DropDownViewModel() { Id = (int)r.Id, Name = r.Name }).ToList();
        }


        // upload user image

        public async Task<ResponseViewModel> UploadUserImage(FileContainerViewModel container)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == container.Id);

                var folderPath = GetUserImageFolderPath(user, _configuration);
                var firstFile = container.Files.FirstOrDefault();

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                if (firstFile != null && firstFile.Length > 0)
                {
                    var fileName = GetUserImageName(user, Path.GetExtension(firstFile.FileName));
                    var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await firstFile.CopyToAsync(stream);
                        user.ProfileImage = filePath;

                        response.IsSuccess = true;
                        response.Message = "User image has been uploaded succesfully";
                    }
                }

                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "User image Upload Failed,Please try again";
            }

            return response;
        }

        private string GetUserImageFolderPath(User model, IConfiguration configuration)
        {
            return string.Format(@"{0}{1}\{2}", configuration.GetSection("FileUploadPath").Value, FolderNames.USER, model.Id);
        }

        public static string GetUserImageName(User model, string extension)
        {
            return string.Format(@"User-Image-{0}-{1}{2}", model.Id, Guid.NewGuid(), extension);
        }

        public static string GetUserImagePath(User model, IConfiguration config, long expenseId)
        {
            return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USER, model.Id);

        }

        //paginated 

        public PaginatedItemsViewModel<BasicUserViewModel> GetUserList(UserFilterViewModel filter)
        {
            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;

            var vmu = new List<BasicUserViewModel>();

            var users = _db.Users.Where(x => x.IsActive == true).OrderBy(u => u.FullName);

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                users = users.Where(x => x.FullName.Contains(filter.SearchText)).OrderBy(u => u.FullName);
            }

            if (filter.RoleId > 0)
            {
                users = users.Where(x => x.UserRoles.Any(x => x.RoleId == filter.RoleId)).OrderBy(x => x.FullName);
            }


            totalRecordCount = users.Count();
            
            totalPageCount = (int)Math.Ceiling((Convert.ToDecimal(totalPageCount) /  filter.PageSize));

            var userList = users.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            foreach (var user in userList)
            {
                var vm = new BasicUserViewModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    MobileNumber = user.MobileNumber,
                    UserName = user.UserName,
                    Address = user.Address,
                    CreatedByName = user.CreatedById.HasValue ? user.CreatedBy.FullName : string.Empty,
                    CreatedOn = user.CreatedOn,
                    UpdatedByName = user.UpdatedById.HasValue ? user.UpdatedBy.FullName : string.Empty,
                    UpdatedOn = user.UpdatedOn,
                };
                vmu.Add(vm);
            }

            var container = new PaginatedItemsViewModel<BasicUserViewModel>(filter.CurrentPage, filter.PageSize, totalPageCount, totalRecordCount, vmu);

            return container;

        }

        public async Task<ResponseViewModel> RegisterClient(ClientViewModel vm)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == vm.Id);

                var getUserRoles = _db.Roles.FirstOrDefault(x => x.IsActive == true);


                if (user == null)
                {
              
                    user = new User()
                    {
                        Id = vm.Id,
                        FullName = vm.FullName,
                        Email = vm.Email,
                        MobileNumber = vm.MobileNumber,
                        UserName = vm.UserName,
                        Address = vm.Email,
                        Password = vm.Password,
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        CreatedById = 1,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedById = 1
                    };

                    user.UserRoles = new HashSet<UserRole>();

                   
                        var userRole = new UserRole()
                        {
                            RoleId = vm.Roles,
                            IsActive = true,
                            CreatedById = 1,
                            CreatedOn = DateTime.UtcNow,
                            UpdatedById = 1,
                            UpdatedOn = DateTime.UtcNow
                        };

                        user.UserRoles.Add(userRole);
                  

                    _db.Users.Add(user);

                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = UserServiceConstants.NEW_USER_SAVE_SUCCESS_MESSAGE;
                }
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = UserServiceConstants.USER_SAVE_EXCEPTION_MESSAGE; 
            }

            return response;
        }
    }
}



