
using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;
        public WorkflowController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
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
                return NotFound("Workflow step not found.");

            return Ok("Workflow step deleted successfully.");
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
