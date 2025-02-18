using Domain.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Shared.Enums;

namespace Infrastructure.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public string GenerateToken(UserRoleEnum userRole, User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = userRole == UserRoleEnum.Superadmin
        //    ? Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:superadmin_secretKey").Get<string>())
        //    : Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:secretKey").Get<string>());
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        new Claim("Role", userRole.ToString()),
        //        // Add additional claims as needed
        //        new Claim("CustomClaim", "CustomValue")
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //        Audience = _configuration["Jwt:Audience"],
        //        Issuer = _configuration["Jwt:Issuer"]
        //};

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
        public string GenerateToken(UserRoleEnum userRole, User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = userRole == UserRoleEnum.Superadmin
                ? Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:superadmin_secretKey").Get<string>())
                : Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:secretKey").Get<string>());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("Role", userRole.ToString()),
            // Additional claims if needed
            new Claim("CustomClaim", "CustomValue")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Jwt:Audience"], // Set the audience claim
                Issuer = _configuration["Jwt:Issuer"] // Set the issuer claim
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }

}
