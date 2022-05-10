using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.ViewModel.User;
using AgriProductTracking.util;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class AuthService : IAuthService
    {
        #region Private Members
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public AuthService(AgriProductTrackerDbContext _db, IConfiguration _configuration)
        {
            this._db = _db;
            this._configuration = _configuration;
        } 
        #endregion

        #region Public Methods
        public UserTokenViewModel Login(LoginViewModel model)
        {
            var response = new UserTokenViewModel();

            try
            {
                var user = _db.Users.FirstOrDefault(u=>u.UserName.Trim().ToUpper() == model.UserName.Trim().ToUpper() && u.IsActive == true);

                if (user == null)
                {
                    response.IsLoginSuccess = false;
                    response.LoginMessage = "User Not Found";

                    return response;
                }
                var test = CustomPasswordHasher.GenerateHash(model.Password);
                if (model.Password != user.Password)
                {
                    response.IsLoginSuccess = false;
                    response.LoginMessage = "Login failed Username or Password Invalid.";

                    return response;
                }

                var secretKey  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                string userRole = string.Empty;
                string roles = string.Join(",", user.UserRoles.Select(r => r.Role.Name).ToList());

                var now = DateTime.UtcNow;
                DateTime nowDate = DateTime.UtcNow;

                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Aud,"mobileapp"),
                        new Claim(JwtRegisteredClaimNames.Aud,"webapp"),
                        new Claim(ClaimTypes.Role,roles)
                };


                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    claims: claims,
                    notBefore: nowDate,
                    expires: nowDate.AddDays(100),
                    signingCredentials: signinCredentials

                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                response.Token = tokenString;
                response.IsLoginSuccess = true;
                response.DisplayName = user.FullName;
                response.Roles = user.UserRoles.Select(t => t.Role).Select(t => t.Name).ToList();

       
            }
            catch (Exception ex)
            {
                response.IsLoginSuccess = false;
                response.LoginMessage = ex.Message;
            }
            return response;
        } 
        #endregion
    }
}
