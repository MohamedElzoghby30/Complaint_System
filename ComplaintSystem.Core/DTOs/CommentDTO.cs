using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
