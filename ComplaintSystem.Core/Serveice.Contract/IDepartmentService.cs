using ComplaintSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IDepartmentService
    {
        Task<DepartmentDTO> CreateAsync(DepartmentDTO dto);
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
        Task<DepartmentDTO> UpdateAsync(DepartmentDTO dto);
    }
}
