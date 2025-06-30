using ComplaintSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Repository.Contract
{
    public interface ICommentRepository
    {
        Task<bool> AddCommentAsync(CommentComplainer comment);
        Task<IEnumerable<CommentComplainer>> GetCommentsForComplaintAsync(int Id);
    }
}
