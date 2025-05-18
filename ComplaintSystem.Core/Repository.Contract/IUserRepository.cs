using ComplaintSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ComplaintSystem.Core.Repository.Contract
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
  
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<bool> RoleExistsAsync(string role);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<ApplicationUser> FindByIdAsync(int UserId);
    }
}
    