using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Repo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Repository.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

     //   public async Task<Department> IsDepartmentValid(int DeptId) => await _context.Departments.FirstOrDefaultAsync(x=> x.Id == DeptId);
        public Task<ApplicationUser> FindByEmailAsync(string email) => _userManager.FindByEmailAsync(email);
        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password) => _userManager.CheckPasswordAsync(user, password);
        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password) => _userManager.CreateAsync(user, password);
        public Task<bool> RoleExistsAsync(string role) => _roleManager.RoleExistsAsync(role);
        public Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role) => _userManager.AddToRoleAsync(user, role);
        public Task<IList<string>> GetRolesAsync(ApplicationUser user) => _userManager.GetRolesAsync(user);

        public Task<ApplicationUser> FindByIdAsync(int UserId)=> _userManager.Users
            .Include(x => x.AssignedComplaints)
            .FirstOrDefaultAsync(x => x.Id == UserId);
       
    }

}