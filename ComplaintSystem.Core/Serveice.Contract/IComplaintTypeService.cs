using ComplaintSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IComplaintTypeService
    {
        Task<ComplaintTypeDTO> AddComplaintTypeAsync(ComplaintTypeDTO complaintTypeDTO);
        Task<IEnumerable<ComplaintTypeDTO>> GetAllComplaintTypesAsync();
    }
}
