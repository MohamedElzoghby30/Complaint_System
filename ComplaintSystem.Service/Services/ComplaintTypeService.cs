using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Core.Serveice.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplaintSystem.Core.DTOs.ComplaintTypeDTO;

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
        public async Task<ComplaintTypeUpdateDTO> UpdateComplaintTypesAsync(ComplaintTypeUpdateDTO dto)
        {
            var ComplaintType = await _repo.UpdateComplaintTypeAsnc(dto);

            return ComplaintType;
        }

        public async Task<IEnumerable<GetComplaintTypeDTO>> GetAllComplaintTypesAsync()
        {
            var list = await _repo.GetAllAsync();
            var response= new List<GetComplaintTypeDTO>();
            foreach (var item in list)
            {
                if (item.Workflows != null)
                {
                  response.Add(new GetComplaintTypeDTO
                    {
                        Id = item.Id,
                        TypeName = item.TypeName,
                        DepartmentID = item.DepartmentID,
                        DepartmentName = item.Department.DepartmentName
                    });
                }
            }

            return response;
        }
    }
}
