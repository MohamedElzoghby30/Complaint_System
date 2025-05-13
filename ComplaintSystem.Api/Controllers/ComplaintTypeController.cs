using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Serveice.Contract;
using ComplaintSystem.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComplaintSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintTypeController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IComplaintTypeService _complaintTypeService;
        private readonly IDepartmentService _departmentService;
        public ComplaintTypeController(IRoleService roleService, IComplaintTypeService complaintTypeService, IDepartmentService departmentService) 
        {
            _roleService = roleService;
            _complaintTypeService = complaintTypeService;
            _departmentService = departmentService;
        }

       
        [HttpPost("AddComplaintType")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ComplaintTypeDTO>> AddComplaintType([FromBody] ComplaintTypeDTO complaintTypeDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _complaintTypeService.AddComplaintTypeAsync(complaintTypeDTO);
            return Ok(result);
        }

        [HttpGet("Get-All-ComplaintType")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetComplaintTypeDTO>>> GetAllComplaintTypes()
        {
            var result = await _complaintTypeService.GetAllComplaintTypesAsync();
            return Ok(result);
        }

    }
}
