using Application.Contracts.Persistence;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.ServiceInterfaces
{
    public interface IUserService
    {
        //Task<IdentityResult> RegisterUserAsync(RegisterModel model);
        Task<User> RegisterUserAsync(RegisterModel model);
        //Task<string> LoginUserAsync(LoginModel model);
        Task<User> LoginUserAsync(LoginModel model);

        //string GetUserId(ClaimsPrincipal user);
       
    }
}
