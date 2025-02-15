using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.CustomAttributes
{

    public class PermissionAttribute : AuthorizeAttribute
    {
        private readonly string[] _permissions;

        public PermissionAttribute(params string[] permissions)
        {
            _permissions = permissions;
        }

        public async Task<bool> IsAuthorizedAsync(HttpContext httpContext)
        {
            var permissionService = httpContext.RequestServices.GetRequiredService<PermissionService>();
            var user = httpContext.User;
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return false;
            }

            var userPermissions = await permissionService.GetUserPermissionsAsync(userId);
            var userPermissionNames = userPermissions.Select(p => p.Name).ToList();
            return _permissions.All(permission => userPermissionNames.Contains(permission));
        }




    }
    }
