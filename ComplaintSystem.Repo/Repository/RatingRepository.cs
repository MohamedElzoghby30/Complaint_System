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
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ComplaintBelongsToUserAsync(int complaintId, int userId)
        {
            return await _context.Complaints.AnyAsync(c => c.Id == complaintId && c.UserID == userId);
        }

        public async Task<bool> HasRatingAsync(int complaintId)
        {
            return await _context.Ratings.AnyAsync(r => r.ComplaintId == complaintId);
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }
    }

}
