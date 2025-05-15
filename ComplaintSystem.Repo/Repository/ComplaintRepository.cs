using ComplaintSystem.Core;
using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Repo.Data;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Repo.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Complaint> AddAsync(Complaint complaint)
        {
          await  _context.Complaints.AddAsync(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task<bool> ComplaintTypeExistsAsync(int complaintTypeId)
        {
            return await _context.ComplaintTypes.AnyAsync(ct => ct.Id == complaintTypeId);
        }

        public async Task<Workflow> GetFirstWorkflowAsync(int complaintTypeId)
        {
            return await _context.Workflows
                .Where(w => w.ComplaintTypeID == complaintTypeId && w.StepOrder == 1)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId)
        {
            // var totalCount = await _context.Complaints.CountAsync(c => c.UserID == userId);
            return await _context.Complaints
                .Include(c => c.ComplaintType)
                .Include(c => c.User)
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }
<<<<<<< HEAD
        public async Task<PaginatedListCore<Complaint>>GetByUserIdAsync(int userId, int pageNumber, int pageSize)
        {

            //var Count = await _context.Complaints.CountAsync(c => c.UserID == userId);
            //int TotalPages = (int)Math.Ceiling(Count / (double)pageSize);
            //bool HasPreviousPage = pageNumber > 1;
            //bool HasNextPage = pageNumber < TotalPages;
           var complaints= await _context.Complaints
               .Include(c => c.ComplaintType)
               .Include(c => c.User)
               .Where(c => c.UserID == userId)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
            var x = await PaginatedListCore<Complaint>.CreateAsync(complaints, pageNumber, pageSize);

            return  x;
        }
        public async Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId,string status)
=======
        public async Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId,ComplaintStatus status)
>>>>>>> 4b82f19787e90c6ad999eefd760b9df3aaaf9c8e
        {
            return await _context.Complaints
                .Include(c => c.ComplaintType)
                .Include(c => c.User)
                .Where(c => c.UserID == userId)
                .Where (c => c.Status==status)
                .ToListAsync();
        }
        public async Task<Complaint> GetComplaintByIdAsync(int id, int userId)
        {
            return await _context.Complaints
                .Include(c => c.User)
                .Include(c => c.ComplaintType)
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserID == userId);
        }

    }
}
