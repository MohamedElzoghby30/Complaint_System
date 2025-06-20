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
        public int workflowId { get; set; }
        //[Required]
        //public int ComplaintTypeID { get; set; }

        //public int? StepOrder { get; set; }

        //public int? NextStepID { get; set; }
        public int UserId { get; set; }
        public string ? UserName { get; set; }

        [EmailAddress]
        public string? UserEmail { get; set; }
    }
    public class GetWorkflow
    {
        public string StepName { get; set; }
        public int ComplaintTypeID { get; set; }

        public int? StepOrder { get; set; }
        public int? NextStepID { get; set; }
        public int UserId { get; set; }


    }
    public class SwapWorkflowDTO
    {
        public int ComplaintTypeId { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
}

