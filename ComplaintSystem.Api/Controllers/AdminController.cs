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
    public class AdminController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IComplaintTypeService _complaintTypeService;
        private readonly IDepartmentService _departmentService;
        public AdminController(IRoleService roleService, IComplaintTypeService complaintTypeService, IDepartmentService departmentService) 
        {
            _roleService = roleService;
            _complaintTypeService = complaintTypeService;
            _departmentService = departmentService;
        }

        [HttpGet("GetAllRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<RolesDTO>>> GetAllRoles()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            var roles = await _roleService.GetAllRolesAsync();

            if (!roles.Any())
                return NotFound(new { message = "No roles found." });

            return Ok(roles);
        }
        [HttpPut("UpdateRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([FromBody] RolesDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.UpdateRoleAsync(dto);
            if (!result)
                return NotFound(new { message = "Role not found." });

            return Ok(new { message = "Role updated successfully." });
        }
        [HttpPost("CreateUserWithRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUserWithRole([FromBody] CreateUserWithRoleDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, error) = await _roleService.CreateUserAndAssignRoleAsync(dto);
            if (!success)
                return BadRequest(new { message = error });

            return Ok(new { message = "User created and added to role successfully." });
        }
        [HttpPost("AddUserToRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToRole([FromBody] AddUserToRoleDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, error) = await _roleService.AddUserToRoleAsync(dto);
            if (!success)
                return BadRequest(new { message = error });

            return Ok(new { message = "User added to role successfully." });
        }
        [HttpPut("DisableUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisableUser([FromQuery] string email)
        {
            var result = await _roleService.DisableUserByEmailAsync(email);
            if (!result)
                return NotFound("User not found or error occurred.");

            return Ok("User disabled successfully.");
        }
         [HttpPut("EnableUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnableUser([FromQuery] string email)
        {
            var result = await _roleService.EnableUserByEmailAsync(email);
            if (!result)
                return NotFound("User not found or error occurred.");

            return Ok("User enabled successfully.");
        }
        [HttpPost("AddComplaintType")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ComplaintTypeDTO>> AddComplaintType([FromBody] ComplaintTypeDTO complaintTypeDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _complaintTypeService.AddComplaintTypeAsync(complaintTypeDTO);
            return Ok(new { message = "Added successfully", data = result });
        }
        [HttpPost("Create-Department")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment([FromBody] DepartmentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("Update-Department")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DepartmentDTO>> Update([FromBody] DepartmentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _departmentService.UpdateAsync(dto);
            if (updated == null)
                return NotFound(new { message = "Department not found" });

            return Ok(updated);
        }

    }
}
