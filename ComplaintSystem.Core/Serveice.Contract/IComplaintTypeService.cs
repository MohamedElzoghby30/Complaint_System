using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplaintSystem.Core.DTOs.ComplaintTypeDTO;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IComplaintTypeService
    {
        Task<ComplaintTypeDTO> AddComplaintTypeAsync(ComplaintTypeDTO complaintTypeDTO);
        Task<IEnumerable<GetComplaintTypeDTO>> GetAllComplaintTypesAsync();
        Task<ComplaintTypeUpdateDTO> UpdateComplaintTypesAsync(ComplaintTypeUpdateDTO dto);
    }
}
