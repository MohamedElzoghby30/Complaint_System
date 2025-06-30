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
        private readonly IComplaintRepository _complaintRepository;
        public WorkflowService(ApplicationDbContext context, IWorkflowRepo repo, IComplaintTypeRepository complaintTypeRepository, IUserRepository userRepository,IComplaintRepository complaintRepository)
        {
            _context = context;
            _repo = repo;
            _complaintTypeRepository = complaintTypeRepository;
            _userRepository = userRepository;
            _complaintRepository = complaintRepository;
        }

        public async Task<IEnumerable<GetWorkflowStepDTO>> GetWorkflowsByComplaintTypeAsync(int complaintTypeId)
        {
            var WorkflowDB = await _repo.GetWorkflowByComplaintTypeAsync(complaintTypeId);
            return WorkflowDB.Select(d => new GetWorkflowStepDTO
            {
                workflowId= d.Id,
                UserName=d.User?.FullName ?? "No User Assigned",
                UserId= d.UserId??0,
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
            var workflowCurrent = await _repo.GetWorkflowByIdAsync(workflowId);
            if (workflowCurrent == null) return false;
            var complaintType = await _complaintTypeRepository.GetComplaintTypeByIdAsync(workflowCurrent.ComplaintTypeID);
            if (complaintType == null) return false;

           Workflow workflowBefore =  await _context.Workflows.FirstOrDefaultAsync(x=>x.NextStepID==workflowCurrent.Id);

           Workflow workflowAfter = await _context.Workflows.FirstOrDefaultAsync(x=>x.Id == workflowCurrent.NextStepID);
            var Complaints = await _complaintRepository.GetComplaintSByCurrentStep(workflowId);
         
            if (workflowBefore == null && workflowAfter == null)
            {
                // No previous or next step, just delete the workflow
               
                return false;
            }
            else if (workflowBefore != null && workflowAfter != null)
            {
                // Both previous and next steps exist, update the links
                
                workflowBefore.NextStepID = workflowAfter.Id;
                _context.Workflows.Update(workflowBefore);
                await _context.SaveChangesAsync();
               if(Complaints.Any())
               {
                    foreach (var complaint in Complaints)
                    {
                        complaint.CurrentStepID = workflowAfter.Id;
                        complaint.AssignedToID = workflowAfter.UserId;
                        complaint.AssignedAt = DateTime.Now;
                    var x = await _complaintRepository.UpdateComplaintAsync(complaint);   
                    }

               }
                var reslut = await _repo.DeleteWorkflow(workflowCurrent,  complaintType);
                return reslut;
            }
            else if (workflowBefore == null && workflowAfter!=null)
            {
                // Only previous step exists, set its NextStepID to null
                if (Complaints.Any())
                {
                    foreach (var complaint in Complaints)
                    {
                        complaint.CurrentStepID = workflowAfter.Id;
                        complaint.AssignedToID = workflowAfter.UserId;
                        complaint.AssignedAt = DateTime.Now;
                        var x = await _complaintRepository.UpdateComplaintAsync(complaint);
                    }

                }
                var reslut = await _repo.DeleteWorkflow(workflowCurrent, complaintType);
                return reslut;
            }
            else
            {
                // Only next step exists, set its PreviousStepID to null

                workflowBefore.NextStepID = null;
                _context.Workflows.Update(workflowBefore);
                await _context.SaveChangesAsync();
                if (Complaints.Any())
                {
                    foreach (var complaint in Complaints)
                    {
                        complaint.CurrentStepID = workflowBefore.Id;
                        complaint.AssignedToID = workflowBefore.UserId;
                        complaint.AssignedAt = DateTime.Now;
                        var x = await _complaintRepository.UpdateComplaintAsync(complaint);
                    }

                }
                var reslut = await _repo.DeleteWorkflow(workflowCurrent, complaintType);
                return reslut;
            }
           
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
        public async Task<GetWorkflow> GetWorkflowsByIdAsync(int workflowId)
        {
            var workflowDB = await _repo.GetWorkflowByIdAsync(workflowId);
            if (workflowDB == null)
                return null;
           return new GetWorkflow
           {
                StepName = workflowDB.StepName,
                ComplaintTypeID = workflowDB.ComplaintTypeID,
                StepOrder = workflowDB.StepOrder,
                NextStepID = workflowDB.NextStepID,
                UserId = workflowDB.UserId??0
                
        };
           }
        public async Task<Workflow> WorkflowsByIdAsync(int workflowId)
        {
            var workflowDB = await _repo.GetWorkflowByIdAsync(workflowId);
            if (workflowDB == null)
                return null;
            return workflowDB;
        }

    }
 }

