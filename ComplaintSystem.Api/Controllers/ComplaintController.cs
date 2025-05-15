using ComplaintSystem.Api.Extension;
using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Serveice.Contract;
using ComplaintSystem.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace ComplaintSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;
        private readonly IComplaintTypeService _complaintTypeService;
        private readonly IRatingService _ratingService;
        public ComplaintController(IComplaintService complaintService, IComplaintTypeService complaintTypeService, IRatingService ratingService)
        {
            _complaintService = complaintService;
            _complaintTypeService = complaintTypeService;
            _ratingService = ratingService;
        }

        [Authorize(Roles = "Complainer")]
        [HttpPost("createComplaint")]
        public async Task<IActionResult> CreateComplaint([FromBody] AddComplaintDTO complaintDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            (bool succeeded, string[] errors) = await _complaintService.CreateComplaintAsync(complaintDto, userId);

            if (!succeeded)
                return BadRequest(new { Errors = errors });

            return Ok();
        }
        [HttpGet("MyComplaints")]
        [Authorize(Roles = "Complainer")]
        public async Task<ActionResult<IEnumerable<ComplaintDTO>>> GetMyComplaints([FromQuery] ComplaintStatus status)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            if (status == null)
            {
                var complaints = await _complaintService.GetComplaintsForUserAsync(userId);
                var list = complaints.ToList();
               // var page = new PaginatedList<ComplaintDTO>(list, list.Count(), 1, list.Count());
                var x= PaginatedList<ComplaintDTO>.CreateAsync(complaints, 1, 2);
                return Ok(new {x.Result.Items , x.Result.Count,x.Result.HasPreviousPage,x.Result.HasNextPage});
            }
            else
            {
                var complaints = await _complaintService.GetComplaintsForUserAsync(userId,status);
                return Ok(complaints);
            }

           
        }
        [HttpGet("GetComplaintByID")]
        [Authorize(Roles = "Complainer")]
        public async Task<IActionResult> GetComplaint(int id)
        {
          //  var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId) || userId == 0) 
                return Unauthorized();

            var complaint = await _complaintService.GetComplaintAsync(id, userId);

            if (complaint == null)
                return NotFound(new { message = "Complaint not found or not yours." });

            return Ok(complaint);
        }
       

        [HttpGet("GetAllComplaintType")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ComplaintTypeDTO>>> GetAll()
        {
            var list = await _complaintTypeService.GetAllComplaintTypesAsync();
            return Ok(list);
        }
        [HttpPost("AddRating")]
        [Authorize(Roles = "Complainer")]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var result = await _ratingService.CreateRatingAsync(userId, dto);
            if (!result)
                return BadRequest("The complaint is not yours or has already been evaluated.");

            return Ok("Successfully evaluated");
        }
    }
}
