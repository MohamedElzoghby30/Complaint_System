using EasyComplaint.Core.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Data;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Serveice.Contract;
using ComplaintSystem.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using ComplaintSystem.Service.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ComplaintSystem.Api;

namespace ComplaintSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<Role> _roleManager;
        private readonly Token _tokenService;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IEmailService _emailService;
        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Token tokenService, IConfiguration configuration, RoleManager<Role> roleManager, IRoleService roleService,IEmailService emailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _roleService = roleService;
            _emailService = emailService;
        }
      

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegestrationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            if (model.Password != model.ConfirmPassword)
                return BadRequest(new { Message = "Passwords do not match" });

            (bool succeeded, string[] errors) = await _accountService.RegisterAsync(model);
            if (!succeeded)
                return BadRequest(new { Errors = errors });

            return Ok(new { Message = "User registered successfully with Complainer role" });
        }



        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized("User not registered.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized("The Passworrd is Not Valid");
            

             var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(model.Role))
                return Unauthorized("User has no roles assigned.");
            
            var jwtToken = await _tokenService.CreateToken(user, _userManager);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            Console.WriteLine(token);
            return Ok(new { token });
        }
        [HttpPost("forgot-password")] // POST : /api/accounts/ forgot-password
        public async Task<ActionResult<string>> ForgetPassword([FromBody] ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return Ok("If the email exists, a reset link will be sent.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var resetLink = $"https://your-frontend-url/reset-password?email={model.Email}&token={encodedToken}";

            await _emailService.SendEmailAsync(
                to: model.Email,
                subject: "Reset Your Password",
                body: $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>"
            );

            return Ok("Reset password link sent.");
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Invalid request");

            var decodedToken = Uri.UnescapeDataString(model.Token); 
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password reset successfully.");
        }

        [HttpGet("GetUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUsers([FromQuery] string? role, [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            List<ApplicationUser> filteredUsers;

            if (string.IsNullOrEmpty(role))
            {
                
                var allUsers = _userManager.Users.ToList();
                filteredUsers = new List<ApplicationUser>();

                foreach (var user in allUsers)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles == null || !roles.Any())
                        filteredUsers.Add(user);
                }
            }
            else
            {
               
                filteredUsers = (await _userManager.GetUsersInRoleAsync(role)).ToList();
            }

            var totalCount = filteredUsers.Count;

            var pagedUsers = filteredUsers
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    IsActive = user.LockoutEnd == null || user.LockoutEnd <= DateTimeOffset.Now


                })
                .ToList();

            var result = new PagedResult<UserDTO>
            {
                Items = pagedUsers,
                TotalCount = totalCount,
                PageNumber = PageNumber,
                PageSize = PageSize
            };

            return Ok(result);
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
        [HttpPut("Change-Password")]
        [Authorize]
        public async Task<IActionResult> ChanagePassworrd(ChangePassDTO changePassDTO)
        {
           if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if (changePassDTO.NewPassword != changePassDTO.NewPasswordConform)
                return BadRequest(new { Message = "Passwords do not match" });

            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmailClaim.Value);
            if (user == null)
                return NotFound("User not found.");
            else 
            {
                var result = await _userManager.ChangePasswordAsync(user, changePassDTO.lastPassword, changePassDTO.NewPassword);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok("Password changed successfully.");
            }

        }

    }
    //  [HttpPost("login")]

    //public async Task<IActionResult> Login(LoginDTO model)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

    //    (string? token, string[] errors) = await _accountService.LoginAsync(model);
    //    if (token == null)
    //        return BadRequest(new { Errors = errors });

    //    return Ok(new { Token = token, Message = "User logged in successfully" });
    //}





    // لو عايز تستخدم TokenResponseDTO:
    // return Ok(new TokenResponseDTO { Token = token, Message = "User logged in successfully" });
    //    private readonly UserManager<User> _userManager;
    //    private readonly RoleManager<Role> _roleManager;
    //    private readonly IMapper _mapper;

    //    public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager,IMapper mapper)
    //    {
    //        _userManager = userManager;
    //        _roleManager = roleManager;
    //        _mapper = mapper;
    //    }

    //    [HttpPost("register")]
    //    public async Task<IActionResult> Register(RegestrationDTO model)
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        // Check if user already exists
    //        var existingUser = await _userManager.FindByEmailAsync(model.Email);
    //        if (existingUser != null)
    //            return BadRequest(" Email already exists.");

    //        // Create new user
    //        //mapping
    //        //var user = new User
    //        //{
    //        //    FullName = model.FullName,
    //        //    Email = model.Email,
    //        //    DepartmentID = model.DepartmentID,
    //        //    UserName=model.Email
    //        //};
    //        var user = _mapper.Map<User>(model);

    //        var resultCreate = await _userManager.CreateAsync(user, model.Password);
    //        if (!resultCreate.Succeeded)
    //            return BadRequest(resultCreate.Errors);

    //        // Assign the role 'Complainer' by default
    //        if (!await _roleManager.RoleExistsAsync("Complainer"))
    //            return BadRequest("Complainer role doesn't exist.");

    //       var result =  await _userManager.AddToRoleAsync(user, "Complainer");
    //        if (!result.Succeeded)
    //            return BadRequest(result.Errors);

    //        return Ok("User registered successfully with Complainer role.");
    //    }
    //    [HttpPost("login")]
    //    public async Task<IActionResult> Login(LoginDTO model)
    //    {
    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);
    //        var existingUser = await _userManager.FindByEmailAsync(model.Email);
    //        if (existingUser == null || !await _userManager.CheckPasswordAsync(existingUser, model.Password))
    //            return BadRequest(" Email OR Password is Wrong");
    //        else
    //            return Ok("User Login successfully");

    //    }
    //}

}


