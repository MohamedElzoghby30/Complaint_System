﻿using System.ComponentModel.DataAnnotations;

namespace ComplaintSystem.Core.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        

      //  public string Role { get; set; }

    }
}
