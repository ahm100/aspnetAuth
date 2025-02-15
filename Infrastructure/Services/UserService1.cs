//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Application.Contracts.Persistence;
//using Infrastructure.Persistence;
//using Domain.Entities.Concrete;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using static Shared.Enums;
//using Application.Contracts.ServiceInterfaces;

//namespace Infrastructure.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly SignInManager<User> _signInManager;
//        private readonly IConfiguration _configuration;

//        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _configuration = configuration;
//        }

//        public async Task<IdentityResult> RegisterUserAsync(RegisterModel model)
//        {
//            var user = new User { UserName = model.Username};

//            var result = await _userManager.CreateAsync(user, model.Password);

//            if (result.Succeeded)
//            {
//                var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
//                if (!roleResult.Succeeded)
//                {
//                    // Handle role assignment failure
//                    return IdentityResult.Failed(roleResult.Errors.ToArray());
//                }
//            }
           

//            return result;
//        }



//        // karamali
//        internal async Task<string> GenerateJwtTokenAsync(string userRole, string userID)
//        {

//            var key = userRole == UserRoleEnum.Superadmin.ToString()
//            ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:superadmin_secretKey").Get<string>()))
//            : new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:secretKey").Get<string>()));

//            var claims = new List<Claim>
//            {
//                new Claim("Role", userRole.ToString()),
//                new Claim("UserId",userID),
//                new Claim(JwtRegisteredClaimNames.Aud, _configuration.GetSection("Jwt:Issuer").Get<string>())
//            };

//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _configuration.GetSection("Jwt:Issuer").Get<string>(),  // Add the issuer
//                audience: _configuration.GetSection("Jwt:Issuer").Get<string>(),
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(30),
//                signingCredentials: creds);

            

//            var tokenHandler = new JwtSecurityTokenHandler();
//            return tokenHandler.WriteToken(token);
//        }

//        //

//        public async Task<string> LoginUserAsync(LoginModel model)
//            {
//                var user = await _userManager.FindByNameAsync(model.Username);
//            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//            {
//                var userRoles = await _userManager.GetRolesAsync(user);
//                var userId = user.Id;
//                var tokenString = GenerateJwtTokenAsync(userRoles.FirstOrDefault(), userId);
//                    return tokenString.Result;
//                }
//                return null;
//            }

//        public string GetUserId(ClaimsPrincipal user)
//        {
//            return _userManager.GetUserId(user);
//        }


//    }



//}
