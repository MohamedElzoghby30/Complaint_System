using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Entities
{
    public class ComplaintAttachment :BaseEntity
    {
        
        [Required]
        public string FileUrl { get; set; }

        [Required]
        public int ComplaintId { get; set; }

        [ForeignKey("ComplaintId")]
        public Complaint Complaint { get; set; }
    }
}
