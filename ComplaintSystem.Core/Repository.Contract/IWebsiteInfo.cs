using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Repository.Contract
{
    public interface IWebsiteInfo
    {
        Task<WebSiteDetails> GetWebsiteInfoAsync();
        Task<bool> UpdateWebsiteInfoAsync(WebSiteDetails webSiteDetails);
    }
}
