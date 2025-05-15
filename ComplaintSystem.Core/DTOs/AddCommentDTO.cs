using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class AddCommentDTO
    {
        public int ComplaintID { get; set; }
        public string CommentText { get; set; }
    }
}
