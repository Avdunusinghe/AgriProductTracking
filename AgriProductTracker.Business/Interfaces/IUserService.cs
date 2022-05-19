using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business.Interfaces
{
    public interface IUserService
    {
        Task<ResponseViewModel> SaveUser(UserViewModel vm , string userName);
        Task<ResponseViewModel> DeleteUser(long id);
        UserViewModel GetUserbyId(int id);
        List<DropDownViewModel> GetAllRoles();
        Task<ResponseViewModel> UploadUserImage(FileContainerViewModel container);
        PaginatedItemsViewModel<BasicUserViewModel> GetUserList(UserFilterViewModel filter);


    }
}
