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
    public class ComplaintTypeService : IComplaintTypeService
    { 
        private readonly IComplaintTypeRepository _repo;
        public ComplaintTypeService(IComplaintTypeRepository repo)
        {
            _repo = repo;
          
        }

        public async Task<ComplaintTypeDTO> AddComplaintTypeAsync(ComplaintTypeDTO dto)
        {
            var complaintType = new ComplaintType
            {
                TypeName = dto.TypeName,
                DepartmentID = dto.DepartmentID
            };

            var result = await _repo.AddAsync(complaintType);

            return new ComplaintTypeDTO
            {
                TypeName = result.TypeName,
                DepartmentID = result.DepartmentID
            };
        }

        public async Task<IEnumerable<ComplaintTypeDTO>> GetAllComplaintTypesAsync()
        {
            var list = await _repo.GetAllAsync();

            return list.Select(x => new ComplaintTypeDTO
            {
                TypeName = x.TypeName,
                DepartmentID = x.DepartmentID
            });
        }
    }
}
