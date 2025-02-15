using Application.Contracts.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.ServiceInterfaces
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string roleName);
    }
}
