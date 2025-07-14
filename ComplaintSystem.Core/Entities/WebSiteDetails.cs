using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Entities
{
    public class WebSiteDetails :BaseEntity
    {
        [Required(ErrorMessage = "Website name is required")]
        [StringLength(255, ErrorMessage = "Website name must not exceed 255 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(255, ErrorMessage = "Email must not exceed 255 characters")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(50, ErrorMessage = "Phone number must not exceed 50 characters")]
        public string PhoneNumber { get; set; } = null!;
       
        [StringLength(500, ErrorMessage = "Logo URL must not exceed 500 characters")]
        public string? Logo { get; set; }

        [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Governorate is required")]
        [StringLength(100, ErrorMessage = "Governorate must not exceed 100 characters")]
        public string? Governorate { get; set; } 

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City must not exceed 100 characters")]
        public string? City { get; set; } 

        [Url(ErrorMessage = "Invalid Facebook link")]
        [StringLength(500, ErrorMessage = "Facebook link must not exceed 500 characters")]
        public string? FacebookLink { get; set; }

        [Url(ErrorMessage = "Invalid Twitter link")]
        [StringLength(500, ErrorMessage = "Twitter link must not exceed 500 characters")]
        public string? TwitterLink { get; set; }

        [Url(ErrorMessage = "Invalid Instagram link")]
        [StringLength(500, ErrorMessage = "Instagram link must not exceed 500 characters")]
        public string? InstagramLink { get; set; }

        [Url(ErrorMessage = "Invalid LinkedIn link")]
        [StringLength(500, ErrorMessage = "LinkedIn link must not exceed 500 characters")]
        public string? LinkedInLink { get; set; }

        [Url(ErrorMessage = "Invalid YouTube link")]
        [StringLength(500, ErrorMessage = "YouTube link must not exceed 500 characters")]
        public string? YouTubeLink { get; set; }
    }
}
