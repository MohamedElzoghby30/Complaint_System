using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Repository.Contract
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RolesDTO>> GetAllRolesAsync();
        Task<Role> GetByIdAsync(int id);
        Task UpdateAsync(Role role);
    }
}
