using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class ChangePassDTO
    {
        [Required(ErrorMessage = "New Password is required"), MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
       public string lastPassword { get; set; }
        [Required(ErrorMessage = "New Password is required"), MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string NewPasswordConform { get; set; }
    }
}
