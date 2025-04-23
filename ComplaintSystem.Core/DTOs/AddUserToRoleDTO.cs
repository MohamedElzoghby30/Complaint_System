using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class AddUserToRoleDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
