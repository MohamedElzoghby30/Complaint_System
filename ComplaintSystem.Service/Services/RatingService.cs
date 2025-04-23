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
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepo;

        public RatingService(IRatingRepository ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }

        public async Task<bool> CreateRatingAsync(int userId, CreateRatingDTO dto)
        {
            var isOwner = await _ratingRepo.ComplaintBelongsToUserAsync(dto.ComplaintId, userId);
            if (!isOwner)
                return false;

            var alreadyRated = await _ratingRepo.HasRatingAsync(dto.ComplaintId);
            if (alreadyRated)
                return false;

            var rating = new Rating
            {
                ComplaintId = dto.ComplaintId,
                UserId = userId,
                Value = dto.Value
            };

            await _ratingRepo.AddRatingAsync(rating);
            return true;
        }
    }
}

