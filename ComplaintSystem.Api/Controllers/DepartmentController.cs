using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService service)
        {
            _departmentService = service;
        }

        [HttpPost("Create-DepartMent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpGet("Get-All-Department")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _departmentService.GetAllAsync();
            return Ok(result);
        }
        [HttpPut("Update-Department")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update( [FromBody] DepartmentDTO dto)
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
