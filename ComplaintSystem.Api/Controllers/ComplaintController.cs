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
        private readonly IComplaintTypeService _complaintTypeService;
        public ComplaintController(IComplaintService complaintService, IComplaintTypeService complaintTypeService)
        {
            _complaintService = complaintService;
            _complaintTypeService = complaintTypeService;
        }

        [Authorize(Roles = "Complainer")]
        [HttpPost("createComplaint")]
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
        [HttpGet("MyComplaints")]
        [Authorize(Roles = "Complainer")]
        public async Task<IActionResult> GetMyComplaints()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var complaints = await _complaintService.GetComplaintsForUserAsync(userId);
            return Ok(complaints);
        }
        [HttpPost("AddComplaintType")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddComplaintType([FromBody] ComplaintTypeDTO complaintTypeDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _complaintTypeService.AddComplaintTypeAsync(complaintTypeDTO);
            return Ok(new { message = "Added successfully", data = result });
        }

        [HttpGet("GetAllComplaintType")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var list = await _complaintTypeService.GetAllComplaintTypesAsync();
            return Ok(list);
        }
    }
}
