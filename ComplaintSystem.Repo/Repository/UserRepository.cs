using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using Microsoft.AspNetCore.Identity;

namespace ComplaintSystem.Repository.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<ApplicationUser> FindByEmailAsync(string email) => _userManager.FindByEmailAsync(email);
        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password) => _userManager.CheckPasswordAsync(user, password);
        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password) => _userManager.CreateAsync(user, password);
        public Task<bool> RoleExistsAsync(string role) => _roleManager.RoleExistsAsync(role);
        public Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role) => _userManager.AddToRoleAsync(user, role);
        public Task<IList<string>> GetRolesAsync(ApplicationUser user) => _userManager.GetRolesAsync(user);
    }

}