using Domain.Entities.Concrete;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPermissionAsync(ApplicationPermission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationPermission> GetPermissionAsync(string permissionId)
        {
            return await _context.Permissions.FindAsync(permissionId);
        }

        public async Task<List<ApplicationPermission>> GetUserPermissionsAsync(string userId)
        {
            //var userPermissions = await (from p in _context.Permissions
            //                             join urp in _context.UserRolePermissions on p.Id equals urp.PermissionId
            //                             where urp.UserId == userId
            //                             select p.Name).ToListAsync();

            var userPermissions = new List<ApplicationPermission>
                        {
                            new ApplicationPermission { Id = 1, Name = "editUser" },
                            new ApplicationPermission { Id = 1, Name = "deleteUser" },
                            new ApplicationPermission { Id = 1, Name = "createUser" }
                        };

            return userPermissions;


        }
    }
}
