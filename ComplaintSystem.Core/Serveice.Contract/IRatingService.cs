using ComplaintSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface IRatingService
    {
        Task<bool> CreateRatingAsync(int userId, CreateRatingDTO dto);
    }
}
