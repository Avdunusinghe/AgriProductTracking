using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.RestApi.Infrastructure.Services;
using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriProductTracker.RestApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public UserController(IUserService _userService, IIdentityService _identityService)
        {
            this._userService = _userService;
            this._identityService = _identityService;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserViewModel vm)
        {
            var userName = _identityService.GetUserName();
            var response = await _userService.SaveUser(vm, userName);

            return Ok(response);
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var response = _userService.GetUserbyId(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("getAllRoles")]
        public ActionResult GetAllRoles()
        {
            var response = _userService.GetAllRoles();

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);

            return Ok(response);
        }


        

        [HttpPost]
        [Route("getUserList")]
        public ActionResult GetUserList(UserFilterViewModel filter)
        {
            var response = _userService.GetUserList(filter);

            return Ok(response);
        }
        

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("uploadUserImage")]
        public async Task<IActionResult> UploadUserImage()
        {
            var container = new FileContainerViewModel();

            var request = await Request.ReadFormAsync();

            container.Id = int.Parse(request["id"]);
            container.Type = int.Parse(request["type"]);

            foreach (var file in request.Files)
            {
                container.Files.Add(file);
            }

            var response = await _userService.UploadUserImage(container);

            return Ok(response);
        }
        

    }
}

 
