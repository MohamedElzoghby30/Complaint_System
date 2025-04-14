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
    public class ComplaintTypeRepository : IComplaintTypeRepository
    {

        private readonly ApplicationDbContext _context;
        public ComplaintTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ComplaintType> AddAsync(ComplaintType complaintType)
        {
            await _context.ComplaintTypes.AddAsync(complaintType);
            await _context.SaveChangesAsync();
            return complaintType;
        }

        public async Task<IEnumerable<ComplaintType>> GetAllAsync()
        {
            return await _context.ComplaintTypes.Include(c => c.Department).ToListAsync();
        }
    }
}
