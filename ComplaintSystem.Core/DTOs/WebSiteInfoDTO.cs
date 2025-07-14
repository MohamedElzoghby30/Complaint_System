using System.ComponentModel.DataAnnotations;

namespace ComplaintSystem.Core.DTOs
{
    public class WebSiteInfoDTO
    {

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

       
        public string PhoneNumber { get; set; } = null!;
      
       

       
        public string? Logo { get; set; }

      
        public string? Description { get; set; }
      
        public string? Governorate { get; set; } = null!;

       
        public string? City { get; set; } = null!;

      
        public string? FacebookLink { get; set; }

       
        public string? TwitterLink { get; set; }

      
        public string? InstagramLink { get; set; }

      
        public string? LinkedInLink { get; set; }

      
        public string? YouTubeLink { get; set; }
    }
}
