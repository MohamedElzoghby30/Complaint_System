using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComplaintSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [Authorize(Roles = "Complainer")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateComplaint([FromBody] ComplaintDTO complaintDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            (bool succeeded, string[] errors) = await _complaintService.CreateComplaintAsync(complaintDto, userId);

            if (!succeeded)
                return BadRequest(new { Errors = errors });

            return Ok(new { Message = "Complaint created successfully!" });
        }
    }
}
