
using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;
        private readonly UserManager<ApplicationUser> _userManager;
        public WorkflowController(IWorkflowService workflowService,UserManager<ApplicationUser> userManager)
        {
            _workflowService = workflowService;
            _userManager = userManager;
        }

        [HttpGet("GetByComplaintType/{complaintTypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByComplaintType(int complaintTypeId)
        {
            var steps = await _workflowService.GetWorkflowsByComplaintTypeAsync(complaintTypeId);
            return Ok(steps);
        }

        [HttpPost("Create")]
       [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateWorkflowStep([FromBody] CreateWorkflowStepDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _workflowService.CreateWorkflowStepAsync(dto);
            if (!created)
                return BadRequest("ComplaintType or User (by email) not found.");

            return Ok("Workflow step created successfully.");
        }

         [HttpDelete("{workflowId}")]
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int workflowId)
        {
            var deleted = await _workflowService.DeleteWorkflowStepAsync(workflowId);
            if (!deleted)
                return NotFound("Cant Be deleted !");

            return Ok("Workflow step deleted successfully.");
        }
        [HttpPut("Get-Users-For-complaintType")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersForComplaintType([FromQuery] int complaintTypeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var users = await _workflowService.GetWorkflowsByComplaintTypeAsync(complaintTypeId);
            if (users == null)
                return NotFound(new { message = "ComplaintType not found" });
           

            var ActiveUsers =
                users.Select(u => new UserDTO
                {
                   
                    Id = u.UserId,
                    Email = u.UserEmail,
                    FullName = u.UserName,
                }).ToList();

            return Ok(ActiveUsers);
        }
        [HttpPut("SwapUserToUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SwapUserToUser(SwapWorkflowDTO swapWorkflowDTO)
        {
            var WorkflowsDB = await _workflowService.GetWorkflowsByComplaintTypeAsync(swapWorkflowDTO.ComplaintTypeId);
            if (WorkflowsDB == null || !WorkflowsDB.Any())
                return NotFound("No workflows found for the specified complaint type.");
            var Workflow1 = WorkflowsDB.FirstOrDefault(x => x.UserEmail == swapWorkflowDTO.Email1);
            var Workflow2 = WorkflowsDB.FirstOrDefault(x => x.UserEmail == swapWorkflowDTO.Email2);
            if (Workflow1 == null || Workflow2 == null)
                return NotFound("One or both users not found in the workflow steps.");
            var updateUser1= await _workflowService.UpdateWorkflowUserAsync(Workflow1.workflowId,Workflow2.UserEmail);
            var updateUser2 = await _workflowService.UpdateWorkflowUserAsync(Workflow2.workflowId, Workflow1.UserEmail);

            return Ok("User updated successfully for workflow step.");
        }

        [HttpPut("UpdateUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int workflowId, [FromQuery] string userEmail)
        {
            var updated = await _workflowService.UpdateWorkflowUserAsync(workflowId, userEmail);
            if (!updated)
                return NotFound("Workflow or user not found.");

            return Ok("User updated successfully for workflow step.");
        }
    }
}
