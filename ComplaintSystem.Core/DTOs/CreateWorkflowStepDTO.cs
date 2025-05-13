using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class CreateWorkflowStepDTO
    {
        [Required]
        public string StepName { get; set; }
        [Required]
        public int ComplaintTypeID { get; set; }

        public int? StepOrder { get; set; }


        [EmailAddress]
        public string? UserEmail { get; set; }
    }
    public class GetWorkflowStepDTO
    {
        [Required]
        public string StepName { get; set; }
        //[Required]
        //public int ComplaintTypeID { get; set; }

        //public int? StepOrder { get; set; }

        //public int? NextStepID { get; set; }

        [EmailAddress]
        public string? UserEmail { get; set; }
    }

}

