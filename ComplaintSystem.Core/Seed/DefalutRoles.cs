using ComplaintSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Seed;

public static class DefalutRoles
{
    public static async Task SeedRoles(RoleManager<Role> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new Role() { Name = "Admin" });
            await roleManager.CreateAsync(new Role() { Name = "Complainer" });
            await roleManager.CreateAsync(new Role() { Name = "Employee" });
        }

    }
}
