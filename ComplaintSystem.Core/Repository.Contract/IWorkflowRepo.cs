using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Repository.Contract
{
    public interface IWorkflowRepo
    {
       Task<ICollection<Workflow>> GetWorkflowByComplaintTypeAsync(int complaintTypeId);
        Task<Workflow> GetWorkflowByIdAsync(int WorkfloId);
        Task<Workflow> AddWorkflowAsync(Workflow workflow, Workflow workflowLast,ComplaintType complaintType);
        Task<bool> AddWorkflowNextStepIdAsync(Workflow workflow, Workflow workflowLast, ComplaintType complaintType);
        Task<bool> DeleteWorkflow(Workflow workflow, ComplaintType complaintType);
        Task<bool> UpdateWorkflowUserAsync(int UserId, int worflowId);

    }
}
