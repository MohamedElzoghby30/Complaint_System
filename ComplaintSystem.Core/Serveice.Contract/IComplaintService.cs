using ComplaintSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IComplaintService
    {
        Task<(bool Succeeded, string[] Errors)> CreateComplaintAsync(AddComplaintDTO complaintDto, int userId);
        Task<IEnumerable<ComplaintDTO>> GetComplaintsForUserAsync(int userId);
        Task<ComplaintDTO> GetComplaintAsync(int id, int userId);
    }
}
