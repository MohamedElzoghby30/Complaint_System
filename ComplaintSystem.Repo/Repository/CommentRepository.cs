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
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<bool> AddCommentAsync(CommentComplainer comment)
        {
           
            await _context.CommentComplainers.AddAsync(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CommentComplainer>> GetCommentsForComplaintAsync(int Id)=>
             _context.CommentComplainers.Where(x=>x.ComplaintID==Id).Include(x=>x.User).ToList();
       
    }
}
