using System.ComponentModel.DataAnnotations;

namespace ComplaintSystem.Core.DTOs
{
    public class RegestrationDTO
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Full name must be between 1 and 200 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Department ID is required.")]
        //public int DepartmentID { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
