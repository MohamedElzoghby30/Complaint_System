using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IDepartmentRepository _departmentRepository;

        public RoleService(IRoleRepository roleRepository, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, IDepartmentRepository departmentRepository)
        {
            _roleRepository = roleRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _departmentRepository = departmentRepository;
        }
        public async Task<IEnumerable<RolesDTO>> GetAllRolesAsync() 
        {
           
                return await _roleRepository.GetAllRolesAsync();
           
        }
        public async Task<bool> UpdateRoleAsync(RolesDTO dto)
        {
            var role = await _roleRepository.GetByIdAsync(dto.Id);
            if (role == null) return false;

            role.Name = dto.RoleName;
            await _roleRepository.UpdateAsync(role);
            return true;
        }
        public async Task<(bool Success, string Error)> AddUserToRoleAsync(AddUserToRoleDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null)
                return (false, "User not found.");

            var roleExists = await _roleManager.RoleExistsAsync(dto.RoleName);
            if (!roleExists)
                return (false, "Role does not exist.");

            var result = await _userManager.AddToRoleAsync(user, dto.RoleName);
            if (!result.Succeeded)
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));

            return (true, null);
        }
        public async Task<(bool Success, string Error)> CreateUserAndAssignRoleAsync(CreateUserWithRoleDTO dto)
        {
            var roleExists = await _roleManager.RoleExistsAsync(dto.RoleName);
            if (!roleExists)
                return (false, "Role does not exist.");
          var Dept = _departmentRepository.GetByIdAsync(dto.DepartmentId);
            if (Dept == null)
                return (false, "Department does not exist.");

            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                UserName = dto.Email,
                Email = dto.Email,
                DepartmentID= dto.DepartmentId
               
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));

            var addToRoleResult = await _userManager.AddToRoleAsync(user, dto.RoleName);
            if (!addToRoleResult.Succeeded)
                return (false, string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));

            return (true, null);
        }
        public async Task<bool> DisableUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100); // تمنعه لفترة طويلة

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> EnableUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            user.LockoutEnd = null;
            user.LockoutEnabled = false;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

    }
}
