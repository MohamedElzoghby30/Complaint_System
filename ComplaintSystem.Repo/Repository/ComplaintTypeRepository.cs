using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Repo.Data;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ComplaintTypeUpdateDTO> UpdateComplaintTypeAsnc(ComplaintTypeUpdateDTO dto)
        {
            var complaintType = await _context.ComplaintTypes.FindAsync(dto.Id);
            if (complaintType == null)
            {
                return null; // Or throw an exception
            }

            complaintType.TypeName = dto.TypeName;
           

            _context.ComplaintTypes.Update(complaintType);
            await _context.SaveChangesAsync();

            return new ComplaintTypeUpdateDTO
            {
                Id = complaintType.Id,
                TypeName = complaintType.TypeName,
        
            };
        }
        public async Task<IEnumerable<ComplaintType>> GetAllAsync()
        {
            return await _context.ComplaintTypes.Include(c => c.Department).Include(x=>x.Workflows).ToListAsync();
        }

        public async Task<ComplaintType> GetComplaintTypeByIdAsync(int ComplaintTypeId)
        {
            return  _context.ComplaintTypes.Include(x => x.Workflows).FirstOrDefault(x=>x.Id==ComplaintTypeId);
                
        }
    }
}
