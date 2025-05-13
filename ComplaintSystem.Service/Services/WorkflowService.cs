using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Core.Serveice.Contract;
using ComplaintSystem.Repo.Data;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Service.Services
{
    public class WorkflowService :IWorkflowService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWorkflowRepo _repo;
        private readonly IComplaintTypeRepository _complaintTypeRepository;
        private readonly IUserRepository _userRepository;
        public WorkflowService(ApplicationDbContext context, IWorkflowRepo repo, IComplaintTypeRepository complaintTypeRepository, IUserRepository userRepository)
        {
            _context = context;
            _repo = repo;
            _complaintTypeRepository = complaintTypeRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetWorkflowStepDTO>> GetWorkflowsByComplaintTypeAsync(int complaintTypeId)
        {
            var WorkflowDB = await _repo.GetWorkflowByComplaintTypeAsync(complaintTypeId);
            return WorkflowDB.Select(d => new GetWorkflowStepDTO
            {
                StepName = d.StepName,
                UserEmail=d.User.Email

            });


        }

        public async Task<bool> CreateWorkflowStepAsync(CreateWorkflowStepDTO dto)
        {
            var complaintType = await _complaintTypeRepository.GetComplaintTypeByIdAsync(dto.ComplaintTypeID);
            if (complaintType == null) return false;
            ApplicationUser user = null;
            if (!string.IsNullOrEmpty(dto.UserEmail))
            {
                user = await _userRepository.FindByEmailAsync(dto.UserEmail);
                if (user == null) return false;
            }
            complaintType.Workflows= await _repo.GetWorkflowByComplaintTypeAsync(complaintType.Id);
            var workflow = new Workflow
            {
                StepName = dto.StepName,
                ComplaintTypeID = dto.ComplaintTypeID,
                StepOrder = dto.StepOrder,
                UserId = user?.Id
            };
            Workflow workflowLast = null;
            if (complaintType.Workflows.Count!=0)
            {
                 workflowLast = complaintType.Workflows.Last();
            }
            var reslut= await _repo.AddWorkflowAsync(workflow, workflowLast, complaintType);
            if (reslut == null) return false;
            var Nextstep = await _repo.AddWorkflowNextStepIdAsync(workflow, workflowLast, complaintType);
            return Nextstep;
        }

        public async Task<bool> DeleteWorkflowStepAsync(int workflowId)
        {
            var workflow = await _repo.GetWorkflowByIdAsync(workflowId);
            if (workflow == null) return false;
            var complaintType = await _complaintTypeRepository.GetComplaintTypeByIdAsync(workflow.ComplaintTypeID);
            if (complaintType == null) return false;
            Workflow workflowLast = null;
            if (complaintType.Workflows.Count != 0)
            {
                workflowLast = await _context.Workflows.FirstOrDefaultAsync(w => w.NextStepID == workflow.Id);

            }
            var reslut = await _repo.DeleteWorkflow(workflow, workflowLast, complaintType);
            return reslut;
        }

        public async Task<bool> UpdateWorkflowUserAsync(int workflowId, string userEmail)
        {
            var workflow = await _repo.GetWorkflowByIdAsync(workflowId);
            if (workflow == null) return false;

            var user = await _userRepository.FindByEmailAsync(userEmail);
            if (user == null) return false;

            var reslut = await _repo.UpdateWorkflowUserAsync(user.Id, workflow.Id);
            return reslut;
        }

    }
}
