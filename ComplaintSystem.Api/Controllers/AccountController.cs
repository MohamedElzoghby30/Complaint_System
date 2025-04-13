using EasyComplaint.Core.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Data;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Serveice.Contract;
using ComplaintSystem.Core.DTOs;
using Microsoft.AspNetCore.Authorization;

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
        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Token tokenService, IConfiguration configuration, RoleManager<Role> roleManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _tokenService = tokenService;
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
                return Unauthorized("User not found.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid credentials.");
            // var roles = await _userManager.GetRolesAsync(user);
            var jwtToken = await _tokenService.CreateToken(user, _userManager);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            Console.WriteLine(token);
            return Ok(new { token });
        }
        [Authorize]
        [HttpGet("test")]
        public IActionResult test()
        {
            return Ok(new { Success = true });
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


