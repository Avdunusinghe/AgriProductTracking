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
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class UserService : IUserService
    {
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;

        public UserService(AgriProductTrackerDbContext _db , IConfiguration _configuration)
        {
            this._db = _db;
            this._configuration = _configuration;
        }

        // Save User Service
        public async Task<ResponseViewModel> SaveUser(UserViewModel vm)
        {
            var response = new ResponseViewModel();
            try
            {
              

                var user = _db.Users.FirstOrDefault(x => x.Id == vm.Id);

                var getUserRoles = _db.Roles.FirstOrDefault(x => x.IsActive == true);


                if (user == null)
                {
                    var exisistingUserName =_db.Users.FirstOrDefault(u => u.UserName.Trim().ToUpper() == vm.UserName.Trim().ToUpper());

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
                        CreatedById = vm.Id,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedById = vm.Id
                    };

                    user.UserRoles = new HashSet<UserRole>();

                    foreach (var item in vm.Roles)
                    {
                        var userRole = new UserRole()
                        {
                            RoleId = item,
                            IsActive = true,
                            CreatedById = vm.Id,
                            CreatedOn = DateTime.UtcNow,
                            UpdatedById = vm.Id,
                            UpdatedOn = DateTime.UtcNow
                        };

                        user.UserRoles.Add(userRole);
                    }

                    _db.Users.Add(user);

                    EmailHelper.SendRegisterted(vm.Email, vm.UserName, vm.Password);
                    response.IsSuccess = true;
                    response.Message = UserServiceConstants.NEW_USER_SAVE_SUCCESS_MESSAGE;
                }
                else
                {
                    user.Address = vm.Address;
                    user.FullName = vm.FullName;
                    user.Email = vm.Email;
                    user.MobileNumber = vm.MobileNumber;
                   user.UpdatedById = vm.Id;
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
                            CreatedById = vm.Id,
                            CreatedOn = DateTime.UtcNow,
                            UpdatedById = vm.Id,
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

        // Delete User service
        public async Task<ResponseViewModel> DeleteUser(int id)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == id);

                user.IsActive = false;

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = UserServiceConstants.EXISTING_USER_DELETE_SUCCESS_MESSAGE;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = UserServiceConstants.EXISTING_USER_DELETE_EXCEPTION_MESSAGE;
            }

            return response;
        }

        //GetUserById

        public UserViewModel GetUserbyId(int id)
        {
            var response = new UserViewModel();

            var user = _db.Users.FirstOrDefault(x => x.Id == id);


            response.Id = (int)user.Id;
            response.FullName = user.FullName;
            response.UserName = user.UserName;
            response.Address = user.Address;
            response.Email = user.Email;
            response.MobileNumber = user.MobileNumber;
            response.IsActive = user.IsActive;
            response.ProfileImage = user.ProfileImage;

            var assignedRoles = user.UserRoles.Where(x => x.IsActive == true);

            foreach (var item in assignedRoles)
            {
                response.Roles.Add((int)item.RoleId);
            }

            return response;
        }

        //Get All roles

        public List<DropDownViewModel> GetAllRoles()
        {
            return _db.Roles.Where(x => x.IsActive == true).Select(r => new DropDownViewModel() { Id = (int)r.Id, Name = r.Name }).ToList();
        }
    }
    }



