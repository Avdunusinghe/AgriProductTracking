using AgriProductTracker.Business;
using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.RestApi.Infrastructure.Services;
using AgriProductTracker.ViewModel.User;
using Microsoft.AspNetCore.Mvc;

namespace AgriProductTracker.RestApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }


        [HttpGet]
        [Route("getUserById/{id}")]
        public ActionResult GetUserById(int id)
        {
            var response = _userService.GetUserbyId;
            return Ok(response);
        }

       // [HttpPost]
        //public async Task<ActionResult> Post([FromBody] UserViewModel vm)
       // {
            //var userName = IdentityService.GetUserName();
           // var response = await _userService.SaveUser(vm, us);

          //  return Ok(response);
        //}

         [HttpGet]
        [Route("getAllRoles")]
        public IActionResult GetAllRoles()
        {
            var response = _userService.GetAllRoles();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _userService.DeleteUser(id);

            return Ok(response);
        }
    }
}
