using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Serveice.Contract;
using Microsoft.AspNetCore.Authorization;

namespace ComplaintSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSiteInfoController : ControllerBase
    {
        private readonly IWebsiteInfoService _websiteInfo;

        public WebSiteInfoController(IWebsiteInfoService websiteInfo)
        {
            _websiteInfo = websiteInfo;
        }
        [HttpGet("/GetWebInfo")]
        public async Task<IActionResult> GetWebSiteInf()
        {
            var websiteInfo = await _websiteInfo.GetWebsiteInfo();
            return Ok(websiteInfo);

        }
        [HttpPut("/UpdateWebInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpadteWebInfo([FromBody] WebSiteInfoDTO webSiteInfoDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var reslut=   await _websiteInfo.UpdateWebsiteInfo(webSiteInfoDTO);
                if (!reslut)
                {
                    return BadRequest(new { Errors = new[] { "Failed to update website information." } });
                }
                return Ok(new { Message = "Website information updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }
    }
}
