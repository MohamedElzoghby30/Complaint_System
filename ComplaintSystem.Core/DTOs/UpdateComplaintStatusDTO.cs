using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class UpdateComplaintStatusDTO
    {
       
            public int ComplaintId { get; set; }
            public ComplaintStatus Status { get; set; }
    }
}
