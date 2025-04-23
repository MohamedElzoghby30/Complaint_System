using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.DTOs
{
    public class CreateRatingDTO
    {
        [Required]
        public int ComplaintId { get; set; }

        [Required]
        [EnumDataType(typeof(RatingValueEnum))]
        public RatingValueEnum Value { get; set; }
    }
}
