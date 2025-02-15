using Application.Contracts.ServiceInterfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RoleService: IRoleService
    {
        //private readonly RoleManager<ApplicationRole> _roleManager;
        

        //public RoleService(RoleManager<ApplicationRole> roleManager)
        //{
        //    _roleManager = roleManager;
           
        //}

        public async Task CreateRoleAsync(string roleName)
        {
            //if (!await _roleManager.RoleExistsAsync(roleName))
            //{
            //    var role = new ApplicationRole { Name = roleName };
            //    await _roleManager.CreateAsync(role);
            //}
        }
    }
}
