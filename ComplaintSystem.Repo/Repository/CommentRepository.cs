using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Repo.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<bool> AddCommentAsync(Comment comment)
        {
            await  _context.Comments.AddAsync(comment);

          return await _context.SaveChangesAsync()>0;
        }
    }
}
