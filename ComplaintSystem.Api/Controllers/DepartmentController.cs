using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly UserManager<ApplicationUser> _userManager;
        public DepartmentController(IDepartmentService service,UserManager<ApplicationUser> userManager)
        {
            _departmentService = service;
            _userManager = userManager;
        }

        [HttpGet("Get-All-Department")]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
        {
            var result = await _departmentService.GetAllAsync();
            return Ok(result);
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
        [HttpPut("Get-Users-For-Department")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersForDepartment([FromQuery] int DepartmentID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           

            var users = _userManager.Users.Where(x=>x.DepartmentID==DepartmentID).ToList() ;
            if (users == null)
                return NotFound(new { message = "Department not found" });

            var ActiveUsers = 
                users.Select(u => new UserDTO
                {
                      Id = u.Id,
                      Email = u.Email,
                      FullName = u.FullName,
                      IsActive = u.LockoutEnd == null || u.LockoutEnd <= DateTimeOffset.Now,

                }).ToList();

            return Ok(ActiveUsers);
        }

    }
}
