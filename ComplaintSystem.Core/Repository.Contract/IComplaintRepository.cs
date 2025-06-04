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
        Task<int> GetComlaintsNumByUserIdAsync(int id);
        Task<Complaint> GetComplaintByIWithEmployeedAsync(int id,int userId);

        Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId,ComplaintStatus status, int pageNumber, int pageSize);
        Task<IEnumerable<Complaint>> Assin(int userId, ComplaintStatus status, int pageNumber = 0, int pageSize = 0);



        Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId,ComplaintStatus status);

        Task<Complaint> GetComplaintByIdAsync(int id, int userId);
        Task<bool> UpdateComplaintAsync(Complaint complaint);


    }
}
