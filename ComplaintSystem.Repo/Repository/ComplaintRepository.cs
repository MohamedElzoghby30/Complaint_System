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
            _context.Complaints.Add(complaint);
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
    }
}
