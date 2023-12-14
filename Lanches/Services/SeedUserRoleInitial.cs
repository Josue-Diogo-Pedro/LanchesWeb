using Microsoft.AspNetCore.Identity;

namespace Lanches.Services;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedRoles()
    {
        IdentityRole role = new IdentityRole();
        IdentityResult roleResult;

        if (!_roleManager.RoleExistsAsync("Member").Result)
        {
            role = new IdentityRole();
            role.Name = "Member";
            role.NormalizedName = "MEMBER";
            roleResult = _roleManager.CreateAsync(role).Result;
        }
        if(!_roleManager.RoleExistsAsync("Admin").Result)
        {
            role = new IdentityRole();
            role.Name = "Admin";
            role.NormalizedName = "ADMIN";
            roleResult = _roleManager.CreateAsync(role).Result;

        }
    }

    public void SeedUsers()
    {
        IdentityUser user = new IdentityUser();
        IdentityResult result;

        if(_userManager.FindByEmailAsync("usuario@localhost").Result is null)
        {
            user.UserName = "usuario@localhost";
            user.Email = "usuario@localhost";
            user.NormalizedUserName = "USUARIO@LOCALHOST";
            user.NormalizedEmail = "USUARIO@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            result = _userManager.CreateAsync(user, "Numsey#2023").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Member").Wait();
            }
        }
        if(_userManager.FindByEmailAsync("admin@admin").Result is null)
        {
            user.UserName = "admin@admin";
            user.Email = "admin@admin";
            user.NormalizedUserName = "ADMIN@ADMIN";
            user.NormalizedEmail = "ADMIN@ADMIN";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp= Guid.NewGuid().ToString();

            result = _userManager.CreateAsync(user, "Numsey#2023").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
