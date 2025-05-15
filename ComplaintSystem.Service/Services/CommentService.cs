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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<bool> AddCommentAsync(AddCommentDTO dto, int ParticipantId)
        {
            var comment = new Comment
            {
                ComplaintID = dto.ComplaintID,
                CommentText = dto.CommentText,
                ParticipantID = ParticipantId,
                CreatAt = DateTime.Now
            };
             
            return await _commentRepository.AddCommentAsync(comment);
        }
    }
}
