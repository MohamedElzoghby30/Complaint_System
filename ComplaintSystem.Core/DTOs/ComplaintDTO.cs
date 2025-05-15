using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class ComplaintDTO
    {
        public int? Id { get; set; }
        public string? ComplaintTypeName { get; set; }
         public int ComplaintTypeID { get; set; }
        public UserInfoDTO? User { get; set; }
      
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public ComplaintStatus Status { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 2000 characters.")]
        public string Description { get; set; }
      //  public List<CommentDTO>? Comments { get; set; } = new();

    }
}
