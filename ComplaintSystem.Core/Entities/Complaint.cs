using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComplaintSystem.Core.Entities
{
    public class Complaint : BaseEntity
    {
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        //[StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public ComplaintStatus Status { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 2000 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 2000 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Complaint type ID is required.")]
        public int ComplaintTypeID { get; set; }

        [ForeignKey("ComplaintTypeID")]
        public ComplaintType ComplaintType { get; set; }

        public int? CurrentStepID { get; set; }
        [ForeignKey("CurrentStepID")]
        public Workflow? CurrentStep { get; set; }

        public int? AssignedToID { get; set; }
        [ForeignKey("AssignedToID")]
        public ApplicationUser? AssignedTo { get; set; }

        public DateTime? AssignedAt { get; set; }
        public Rating? Rating { get; set; }
        public ICollection<CommentComplainer>? CommentsComplainer { get; set; }
        public ICollection<Workflow>? Workflows { get; set; }

       // public ICollection<ComplaintParticipant>? Participants { get; set; } = new List<ComplaintParticipant>();
       // public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

        
     
    }
    public enum ComplaintStatus
    {
        Pending = 0,
        InProgress =1,
        Escalated = 2,
        Dropped = 3,
        Resolved = 4
    }


}
