using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Core.Serveice.Contract;
using EasyComplaint.Core.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ComplaintSystem.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        //  private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Token _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(IUserRepository userRepository,/* IMapper mapper,*/ IConfiguration configuration, Token tokenService, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            // _mapper = mapper;
            _configuration = configuration;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<(bool Succeeded, string[] Errors)> RegisterAsync(RegestrationDTO model)
        {
            var existingUser = await _userRepository.FindByEmailAsync(model.Email);
            if (existingUser!=null)
                return (false, new[] { "Email already exists." });
            if (_userRepository.IsDepartmentValid(model.DepartmentID) != null)
                return (false, new[] { "This Department Not Found" });
            //  var user = _mapper.Map<ApplicationUser>(model);
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                DepartmentID = model.DepartmentID,
            };
            var result = await _userRepository.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            if (!await _userRepository.RoleExistsAsync("Complainer"))
               return (false, new[] { "Complainer role doesn't exist." });

            var roleResult = await _userRepository.AddToRoleAsync(user, "Complainer");
            if (!roleResult.Succeeded)
              return (false, roleResult.Errors.Select(e => e.Description).ToArray());

            return (true, Array.Empty<string>());
        }

        //public async Task<(string? Token, string[] Errors)> LoginAsync(LoginDTO model)
        //{
        //    var user = await _userRepository.FindByEmailAsync(model.Email);
        //    if (user == null || !await _userRepository.CheckPasswordAsync(user, model.Password))
        //        return (null, new[] { "Email or Password is incorrect." });

        //    var roles = await _userRepository.GetRolesAsync(user);
        //    var token = await _tokenService.CreateToken(user, _userManager);

        //    return (token.ToString(), Array.Empty<string>());
        //}
       
    }
}

