using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IWorkflowService
    {
        Task<IEnumerable<GetWorkflowStepDTO>> GetWorkflowsByComplaintTypeAsync(int complaintTypeId);
        Task<bool> CreateWorkflowStepAsync(CreateWorkflowStepDTO dto);
        Task<bool> DeleteWorkflowStepAsync(int workflowId);
        Task<bool> UpdateWorkflowUserAsync(int workflowId, string userEmail);
        Task<GetWorkflow> GetWorkflowsByIdAsync(int workflowId);
        Task<Workflow> WorkflowsByIdAsync(int workflowId);
    }
}
