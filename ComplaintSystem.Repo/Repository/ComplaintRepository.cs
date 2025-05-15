using ComplaintSystem.Core;
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
                .Where(w => w.ComplaintTypeID == complaintTypeId )
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId)
        {
          
            return await _context.Complaints
                .Include(c => c.ComplaintType)
                .Include(c => c.User)
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }
        public async Task<PaginatedListCore<Complaint>>GetByUserIdAsync(int userId, int pageNumber, int pageSize)
        {

            
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
        public async Task<IEnumerable<Complaint>> GetByUserIdAsync(int userId,ComplaintStatus status)
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
               // .Include(c => c.Comments)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserID == userId);
        }
        public async Task<Complaint> GetComplaintByIdAsync(int id)
        => await _context.Complaints.FindAsync(id);
       

    }
}
