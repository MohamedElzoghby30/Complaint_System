﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Entities
{
    public class CommentComplainer : BaseEntity
    {

        [Required(ErrorMessage = "ComplaintID is required.")]
        public int ComplaintID { get; set; }

        [ForeignKey("ComplaintID")]
        public Complaint Complaint { get; set; }

        [Required(ErrorMessage = "UserID is required.")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Comment text is required.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Comment text must be between 1 and 1000 characters.")]
        public string CommentText { get; set; }
        public bool IsForUser { get; set; }
       // [Required(ErrorMessage = "Date created is required.")]
        //public DateTime CreatedAt { get; set; }
    }
}
