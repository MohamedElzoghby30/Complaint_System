using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Entities
{
    public class Rating : BaseEntity
    {
        [Required]
        public RatingValueEnum Value { get; set; }
      
        public int ComplaintId { get; set; }
        [ForeignKey("ComplaintId")]
        public Complaint Complaint { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
    public enum RatingValueEnum
    {
        VeryBad = 1,
        Bad = 2,
        Average = 3,
        Good = 4,
        Excellent = 5
    }
}
