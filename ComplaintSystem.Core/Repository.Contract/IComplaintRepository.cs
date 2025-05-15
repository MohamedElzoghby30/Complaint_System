using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Repository.Contract
{
    public interface IComplaintRepository
    {
        Task<Complaint> AddAsync(Complaint complaint);
        Task<bool> ComplaintTypeExistsAsync(int complaintTypeId);
        Task<Workflow> GetFirstWorkflowAsync(int complaintTypeId);
        Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId);
        Task<Complaint> GetComplaintByIdAsync(int id);

        Task<PaginatedListCore<Complaint>> GetByUserIdAsync(int userId, int pageNumber, int pageSize);
       

        Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId,ComplaintStatus status);

        Task<Complaint> GetComplaintByIdAsync(int id, int userId);


    }
}
