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
        public async Task<IEnumerable<Complaint>> Assin(int userId, ComplaintStatus status, int pageNumber = 0, int pageSize = 0)
        {


            var complaints = await _context.Complaints
                .Include(c => c.ComplaintType)
                .Include(c => c.User)
                .Where(c => c.AssignedToID == userId && c.Status == status)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return complaints;
        }
            public async Task<IEnumerable<Complaint>>GetByUserIdAsync(int userId, ComplaintStatus status,int pageNumber=0, int pageSize=0)
            {

               if (pageNumber <= 0|| pageSize<=0) 
               {
                var complaintss = await _context.Complaints
                .Include(c => c.ComplaintType)
                .Include(c => c.User)
                .Include(c=> c.CommentsComplainer)
                .Where(c => c.UserID == userId && c.Status == status)
                .ToListAsync();
                return complaintss;
               }

            var complaints = await _context.Complaints
                .Include(c => c.ComplaintType)
                .Include(c => c.User)
                .Include(c => c.CommentsComplainer)
                .Where(c => c.UserID == userId&&c.Status==status)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return complaints;
           
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
        public async Task<Complaint> GetComplaintByIWithEmployeedAsync(int id,int UserId)
        => await _context.Complaints.Include(x=>x.Workflows).FirstAsync(x=>x.Id==id);
       
        public async Task<bool> UpdateComplaintAsync(Complaint complaint)
        {
            _context.Complaints.Update(complaint);
            return await _context.SaveChangesAsync() > 0;
        }
      public async Task<int> GetComlaintsNumByUserIdAsync(int id) => 
          await _context.Complaints.Where(c => c.UserID == id) .CountAsync();
               
               
       
    }
}
