using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Repo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Repo.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;
        public RoleRepository(RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context; 
        }
            public async Task<IEnumerable<RolesDTO>> GetAllRolesAsync()
            {
                var roles = _roleManager.Roles.ToList();
                var result = roles.Select(r => new RolesDTO
                {
                    Id = r.Id,
                    RoleName = r.Name
                });
                return await Task.FromResult(result);
            }
        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

    }
}
