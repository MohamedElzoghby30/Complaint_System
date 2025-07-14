using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IWebsiteInfoService
    {
        Task<WebSiteInfoDTO> GetWebsiteInfo();
        Task<bool> UpdateWebsiteInfo(WebSiteInfoDTO webSiteInfoDTO);
    }
}
