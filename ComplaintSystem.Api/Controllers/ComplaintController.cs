﻿using ComplaintSystem.Api.Extension;
using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IRatingService _ratingService;
        private readonly IUserService _userservice;
        private readonly IWorkflowService _workflowService;
        private readonly ICommentService _commentService;
        public ComplaintController(IComplaintService complaintService, IComplaintTypeService complaintTypeService, IRatingService ratingService, IUserService userservice, IWorkflowService workflowService, ICommentService commentService)
        {
            _complaintService = complaintService;
            _complaintTypeService = complaintTypeService;
            _ratingService = ratingService;
            _userservice = userservice;
            _workflowService = workflowService;
            _commentService = commentService;
        }

        [Authorize(Roles = "Admin,Complainer,Employee")]
        [HttpPost("createComplaint")]
        [Consumes("multipart/form-data")] 
        public async Task<IActionResult> CreateComplaint([FromForm] AddComplaintDTO complaintDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var (succeeded, errors) = await _complaintService.CreateComplaintAsync(complaintDto, userId);

            if (!succeeded)
                return BadRequest(new { Errors = errors });

            return Ok(new { Message = "Complaint submitted successfully." });
        }

       

        [HttpGet("MyComplaints")]
        [Authorize(Roles = "Admin,Complainer,Employee")]
        public async Task<ActionResult<IEnumerable<ComplaintDTO>>> GetMyComplaints([FromQuery] ComplaintStatus status,int? PageNumber,int? PageSize )
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
         
                var complaints = await _complaintService.GetComplaintsForUserAsync(userId, status, PageNumber??0,PageSize??0);
                
               // var page = new PaginatedList<ComplaintDTO>(list, list.Count(), 1, list.Count());
                
                return Ok(complaints);
           

        }
        [HttpGet("AssingedComplaints")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<IEnumerable<ComplaintDTO>>> AssingedComplaints([FromQuery] ComplaintStatus status, int? PageNumber, int? PageSize)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var complaints = await _complaintService.AssinComplaint(userId, status, PageNumber ?? 0, PageSize ?? 0);

            // var page = new PaginatedList<ComplaintDTO>(list, list.Count(), 1, list.Count());

            return Ok(complaints);


        }
        [HttpGet("GetComplaintByID")]
        [Authorize(Roles = "Admin,Complainer,Employee")]
        public async Task<IActionResult> GetComplaint(int id)
        {

            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId) || userId == 0) 
                return Unauthorized();

            var role = User.FindFirst(ClaimTypes.Role).Value;
            var complaint = new ComplaintDTO();


            if (role== "Complainer")
            {
                 complaint = await _complaintService.GetComplaintUserAsync(id, userId);
            }
            
            else if(role== "Admin"||role== "Employee")
            {
                complaint = await _complaintService.GetComplaintEmployeeAdminAsync(id, userId);
            }
            else
            {
                return Unauthorized();
            }
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
        [HttpPut("Escalate")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> EscalateComplaintWorkflow([FromQuery] EscalateDTO escalateDTO)
        {
            var userIdClaim = int.Parse((User.FindFirst(ClaimTypes.NameIdentifier)).Value);
            var UserDB= await _userservice.FindByIdAsync(userIdClaim);
            var complaint = await _complaintService.GetComplaintByIdAsync(escalateDTO.ComplaintID, userIdClaim);
            if (complaint == null)
                return NotFound(new { message = "Complaint not found or not yours." });
            var workflowDB =  await _workflowService.WorkflowsByIdAsync(complaint.CurrentStepID?? 0);
        
          var nextStepId = workflowDB.NextStepID;
            if (nextStepId == null)
                return NotFound(new { message = "Cant Escalte" });
           
            complaint.CurrentStepID = workflowDB.NextStepID;
            complaint.AssignedToID = workflowDB.NextStep.UserId;
            complaint.AssignedAt = DateTime.Now;
            //complaint.Status = ComplaintStatus.Escalated;
            var result = await _complaintService.UpdateComplaintAsync(complaint);
            if (!result)
                return BadRequest("Failed to escalate complaint.");
            if(escalateDTO.Comment== null || escalateDTO.Comment == string.Empty)
            { 
                return Ok("Complaint escalated successfully.");
            }
            var Comment =new AddCommentDTO
            {
                ComplaintID = complaint.Id,
               
                CommentText = escalateDTO.Comment,
            };
            var CommentRes = await _commentService.AddCommentForEmployeeAsync(Comment,UserDB.Id);
             if (!CommentRes)
                return BadRequest("Failed to add comment.");

            return Ok("Complaint escalated successfully.");

        }

        [HttpPut("EditStatus")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> EditStatus([FromBody] UpdateComplaintStatusDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var result = await _complaintService.UpdateComplaintStatusAsync(dto, userId);
            if (!result)
                return NotFound();

            return Ok("Status updated successfully.");
        }
    }
}
