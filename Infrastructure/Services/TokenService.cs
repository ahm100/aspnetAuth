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

        public string GenerateToken(string userRole, User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = userRole == UserRoleEnum.Superadmin.ToString()
            ? Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:superadmin_secretKey").Get<string>())
            : Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:secretKey").Get<string>());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Role", userRole.ToString()),
                // Add additional claims as needed
                new Claim("CustomClaim", "CustomValue")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

              

    }

}
