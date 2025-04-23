using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Repository.Contract
{
    public interface IRatingRepository
    {
        Task<bool> ComplaintBelongsToUserAsync(int complaintId, int userId);
        Task<bool> HasRatingAsync(int complaintId);
        Task AddRatingAsync(Rating rating);
    }
}
