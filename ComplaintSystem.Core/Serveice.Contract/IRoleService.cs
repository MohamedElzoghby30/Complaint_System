using ComplaintSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IRoleService
    {
        Task<IEnumerable<RolesDTO>> GetAllRolesAsync();
        Task<bool> UpdateRoleAsync(RolesDTO dto);
        Task<(bool Success, string Error)> AddUserToRoleAsync(AddUserToRoleDTO dto);
        Task<(bool Success, string Error)> CreateUserAndAssignRoleAsync(CreateUserWithRoleDTO dto);
        Task<bool> DisableUserByEmailAsync(string email);
        Task<bool> EnableUserByEmailAsync(string email);
    }
}
