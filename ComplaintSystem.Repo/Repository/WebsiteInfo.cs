using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Repo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Repo.Repository
{
    public class WebsiteInfo : IWebsiteInfo
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public WebsiteInfo(ApplicationDbContext applicationDbContext)
        {
          _applicationDbContext = applicationDbContext;
        }
        public async Task<WebSiteDetails> GetWebsiteInfoAsync()
        {
          return await _applicationDbContext.WebSiteDetails.FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateWebsiteInfoAsync(WebSiteDetails webSiteDetails)
        {
            try
            {
                _applicationDbContext.WebSiteDetails.Update(webSiteDetails);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true ;
        }
    }
}
