using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Department name must be between 1 and 200 characters.")]
        public string DepartmentName { get; set; }
       
    }
}
