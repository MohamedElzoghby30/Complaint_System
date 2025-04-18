
using ComplaintSystem.Core.DTOs;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IAccountService
    {
        Task<(bool Succeeded, string[] Errors)> RegisterAsync(RegestrationDTO model);
      
      // Task<(string? Token, string[] Errors)> LoginAsync(LoginDTO model); 
    }
}

