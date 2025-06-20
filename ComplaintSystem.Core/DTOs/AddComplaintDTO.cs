using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class AddComplaintDTO
    {

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 2000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Complaint type ID is required.")]
        public int ComplaintTypeID { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 2000 characters.")]
        public string Title { get; set; }


    }
}
