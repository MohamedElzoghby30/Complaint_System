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

        public CommentController(ICommentService commentService)
        {
             _commentService = commentService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentDTO dto)
        {
            var paticipantId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var result = await _commentService.AddCommentAsync(dto, paticipantId);

            return result ? Ok("Comment added."): BadRequest("Failed to add comment.");


        }
       
    }
}
