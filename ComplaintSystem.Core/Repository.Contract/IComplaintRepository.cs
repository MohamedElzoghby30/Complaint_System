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
    }
}
