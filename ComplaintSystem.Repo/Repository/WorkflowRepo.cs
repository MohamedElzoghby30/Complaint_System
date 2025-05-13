using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Repo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Repo.Repository
{
    public class WorkflowRepo :IWorkflowRepo
    { 
        private readonly ApplicationDbContext _context;
        public WorkflowRepo(ApplicationDbContext context) 
        {

            _context=context;
        }

        public async Task<Workflow> AddWorkflowAsync(Workflow workflow, Workflow workflowLast, ComplaintType complaintType)
        {

         
            if (workflowLast == null)
            {
                complaintType.Workflows.Add(workflow);
                await _context.Workflows.AddAsync(workflow);
                await _context.SaveChangesAsync();
                return workflow;
            }
           else
           {
                await _context.Workflows.AddAsync(workflow);
              
                await _context.SaveChangesAsync();
                return workflow;
           }
         
        }

        public async Task<bool> AddWorkflowNextStepIdAsync(Workflow workflow, Workflow workflowLast, ComplaintType complaintType)
        {
           var workflowDB=  await GetWorkflowByIdAsync(workflow.Id);
            complaintType.Workflows.Add(workflow);
            workflowLast.NextStepID = workflow.Id;
            _context.Workflows.Update(workflowLast);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task< bool> DeleteWorkflow(Workflow workflow, Workflow workflowLast, ComplaintType complaintType)
        {
            if (workflowLast != null)
            {
                workflowLast.NextStepID =workflow.NextStepID;
                _context.Workflows.Update(workflowLast);
            }
            complaintType.Workflows.Remove(workflow);
            _context.Workflows.Remove(workflow);
         await   _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Workflow>> GetWorkflowByComplaintTypeAsync(int complaintTypeId)
        {
            return await _context.Workflows
                .Where(w => w.ComplaintTypeID == complaintTypeId)
                .Include(w => w.User)
                .ToListAsync();
        }
       
        public async Task<Workflow> GetWorkflowByIdAsync(int WorkfloId) 
            => await _context.Workflows.FindAsync(WorkfloId);

        public  async Task<bool> UpdateWorkflowUserAsync(int UserId,int worflowId)
        {
            var workflow = await _context.Workflows.FindAsync(worflowId);
            workflow.UserId = UserId;
            _context.SaveChanges();
            return true;

        }
    }
}
