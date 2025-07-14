using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Core.Serveice.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Service.Services
{
    public class WebsiteInfoService : IWebsiteInfoService
    {
        private readonly IWebsiteInfo _websiteInfo;

        public WebsiteInfoService(IWebsiteInfo websiteInfo)
        {
           _websiteInfo = websiteInfo;
        }
        public async Task<WebSiteInfoDTO> GetWebsiteInfo()
        {
            var websiteInfo = await _websiteInfo.GetWebsiteInfoAsync();
           if (websiteInfo == null)
           {
                throw new Exception("Website information not found");
           }
           return (new WebSiteInfoDTO
           {
                Name = websiteInfo.Name,
                Email = websiteInfo.Email,
                PhoneNumber = websiteInfo.PhoneNumber,
                Logo = websiteInfo.Logo,
                Description = websiteInfo.Description,
                Governorate = websiteInfo.Governorate,
                City = websiteInfo.City,
                FacebookLink = websiteInfo.FacebookLink,
                TwitterLink = websiteInfo.TwitterLink,
                InstagramLink = websiteInfo.InstagramLink,
                LinkedInLink = websiteInfo.LinkedInLink,
                YouTubeLink = websiteInfo.YouTubeLink
           });

        }

        public async Task<bool> UpdateWebsiteInfo(WebSiteInfoDTO webSiteInfoDTO)
        {
            var websiteInfo = await _websiteInfo.GetWebsiteInfoAsync();
            if (websiteInfo == null)
            {
                throw new Exception("Website information not found");
            }
            websiteInfo.Name= webSiteInfoDTO.Name ;
            websiteInfo.Email= webSiteInfoDTO.Email;
            websiteInfo.PhoneNumber= webSiteInfoDTO.PhoneNumber;
            websiteInfo.Logo= webSiteInfoDTO.Logo;
            websiteInfo.Description= webSiteInfoDTO.Description;
            websiteInfo.Governorate= webSiteInfoDTO.Governorate;
            websiteInfo.City= webSiteInfoDTO.City;
            websiteInfo.FacebookLink= webSiteInfoDTO.FacebookLink;
            websiteInfo.TwitterLink= webSiteInfoDTO.TwitterLink;
            websiteInfo.InstagramLink =webSiteInfoDTO.InstagramLink;
            websiteInfo.LinkedInLink= webSiteInfoDTO.LinkedInLink;
            websiteInfo.YouTubeLink=webSiteInfoDTO.YouTubeLink;
            var UpdatedWebsiteInfo = await _websiteInfo.UpdateWebsiteInfoAsync(websiteInfo);

            return UpdatedWebsiteInfo;
        }
    }
}
