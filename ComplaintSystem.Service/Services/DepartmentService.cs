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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<DepartmentDTO> CreateAsync(DepartmentDTO dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName
            };

            var created = await _repo.AddAsync(department);

            return new DepartmentDTO
            {
                Id = created.Id,
                DepartmentName = created.DepartmentName
            };
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            var departments = await _repo.GetAllAsync();
            return departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName
            });
        }
        public async Task<DepartmentDTO> UpdateAsync( DepartmentDTO dto)
        {
            var department = await _repo.GetByIdAsync(dto.Id);
            if (department == null) return null;

            department.DepartmentName = dto.DepartmentName;
            await _repo.UpdateAsync(department);

            return new DepartmentDTO
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName
            };
        }
    }
}
