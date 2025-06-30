using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComplaintSystem.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IComplaintService _complaintService;

        public CommentController(ICommentService commentService, IComplaintService complaintService)
        {
             _commentService = commentService;
            _complaintService = complaintService;
        }

        [HttpPost("Add-Comment-For-Employee")]
        [Authorize(Roles = "Admin,Complainer,Employee")]
        public async Task<IActionResult> AddCommentForEmployee([FromBody] AddCommentDTO dto)
        {
            var UserIDClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var UserID = int.Parse(UserIDClaim.Value);
            var complaintDB = await _complaintService.GetComplaintByIdAsync(dto.ComplaintID, UserID);
            if (complaintDB.AssignedToID != UserID&&complaintDB.UserID != UserID)
                return Unauthorized("Can not Comment.");    

            var result = await _commentService.AddCommentForEmployeeAsync(dto, UserID);
            if (!result)
                return BadRequest("Failed to add comment.");

            return Ok("Comment added.");


        }
        [HttpPost("Add-Comment-For-User")]
        [Authorize(Roles = "Admin,Complainer,Employee")]
        public async Task<IActionResult> AddCommentForUser([FromBody] AddCommentDTO dto)
        {
            var UserIDClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var UserID = int.Parse(UserIDClaim.Value);
            var complaintDB = await _complaintService.GetComplaintByIdAsync(dto.ComplaintID, UserID);
            if (complaintDB.AssignedToID != UserID && complaintDB.UserID != UserID)
                return Unauthorized("Can not Comment.");

            var result = await _commentService.AddCommentForUserAsync(dto, UserID);
            if (!result)
                return BadRequest("Failed to add comment.");

            return Ok("Comment added.");


        }

    }
}
