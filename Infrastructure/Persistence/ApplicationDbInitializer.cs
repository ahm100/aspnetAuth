using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbInitializer
    {
        //public static async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
        //{
        //    string[] roleNames = { "Superadmin", "Admin", "User" };
        //    IdentityResult roleResult;

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExist = await roleManager.RoleExistsAsync(roleName);
        //        if (!roleExist)
        //        {
        //            var role = new ApplicationRole { Name = roleName };
        //            roleResult = await roleManager.CreateAsync(role);
        //        }
        //    }
        //}
    }

}
