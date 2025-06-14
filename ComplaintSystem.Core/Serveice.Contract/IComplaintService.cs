using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
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
        Task<PaginatedListCore<Complaint,ComplaintDTO>> GetComplaintsForUserAsync(int userId, ComplaintStatus status, int pageNumber, int PageSize);
        Task<Complaint> GetComplaintByIdAsync(int Id,int UserID);

        Task<PaginatedListCore<Complaint, ComplaintDTO>> AssinComplaint(int userId, ComplaintStatus status = ComplaintStatus.Pending, int pageNumber = 1, int PageSize = 10);
        Task<ComplaintDTO> GetComplaintAsync(int id, int userId);
        Task<bool> UpdateComplaintAsync(Complaint complaint);
        // Task<Complaint> GetComplaintByIdAsync(int Id, int UserID);
        Task<bool> UpdateComplaintStatusAsync(UpdateComplaintStatusDTO dto, int userId);
    }
}
