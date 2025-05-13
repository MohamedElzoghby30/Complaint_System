using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Entities
{
    public class ApplicationUser :IdentityUser<int>
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Full name must be between 1 and 200 characters.")]
        public string FullName { get; set; }
        public int? DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }
        public ICollection<Workflow>? Workflows { get; set; } = new List<Workflow>();
        public ICollection<CommentComplainer>? CommentsComplainer { get; set; }= new List<CommentComplainer>();
        public ICollection<Complaint>? Complaints { get; set; } = new List<Complaint>();
        public ICollection<Complaint>? AssignedComplaints { get; set; } = new List<Complaint>();
    }
}
