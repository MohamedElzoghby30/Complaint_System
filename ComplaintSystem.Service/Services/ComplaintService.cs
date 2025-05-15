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
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
      //  private readonly IMapper _mapper;

        public ComplaintService(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
          //  _mapper = mapper;
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateComplaintAsync(AddComplaintDTO complaintDto, int userId)
        {
            if (string.IsNullOrWhiteSpace(complaintDto.Description))
                return (false, new[] { "Description is required." });

            if (!await _complaintRepository.ComplaintTypeExistsAsync(complaintDto.ComplaintTypeID))
                return (false, new[] { "Invalid ComplaintTypeID." });

            // var complaint = _mapper.Map<Complaint>(complaintDto);
            var complaint = new Complaint()
            {
                Description = complaintDto.Description,
                ComplaintTypeID=complaintDto.ComplaintTypeID,
                UserID = userId,
                Status = ComplaintStatus.Pending
            };
            //complaint.UserID = userId;
            //complaint.Status = "Pending";

            // جيب أول Workflow للـ ComplaintType
            var firstWorkflow = await _complaintRepository.GetFirstWorkflowAsync(complaintDto.ComplaintTypeID);
            if (firstWorkflow == null)
                return (false, new[] { "No workflow defined for this complaint type." });

            complaint.CurrentStepID = firstWorkflow.Id;

            await _complaintRepository.AddAsync(complaint);
            return (true, Array.Empty<string>());
        }
        public async Task<IEnumerable<ComplaintDTO>> GetComplaintsForUserAsync(int userId)
        {
            var complaints = await _complaintRepository.GetByUserIdAsync(userId);

            return complaints.Select(c => new ComplaintDTO
            {
                Id = c.Id,
                Status = c.Status,
                Description = c.Description,
                ComplaintTypeName = c.ComplaintType?.TypeName,
                ComplaintTypeID = c.ComplaintTypeID,
                User = new UserInfoDTO
                {
                    FullName = c.User?.FullName,
                    Email = c.User?.Email
                }
            });
        }
        public async Task<IEnumerable<ComplaintDTO>> GetComplaintsForUserAsync(int userId,ComplaintStatus status)
        {
            var complaints = await _complaintRepository.GetByUserIdAsync(userId, status);

            return complaints.Select(c => new ComplaintDTO
            {
                Id = c.Id,
                Status = c.Status,
                Description = c.Description,
                ComplaintTypeName = c.ComplaintType?.TypeName,
                ComplaintTypeID = c.ComplaintTypeID,
                User = new UserInfoDTO
                {
                    FullName = c.User?.FullName,
                    Email = c.User?.Email
                }
            });
        }
        public async Task<ComplaintDTO> GetComplaintAsync(int id, int userId)
        {
            var complaintDB = await _complaintRepository.GetComplaintByIdAsync(id, userId);
            if (complaintDB == null) return null;

            return new ComplaintDTO
            {
                Id = complaintDB.Id,
                Status = complaintDB.Status,
                Description = complaintDB.Description,
                ComplaintTypeName = complaintDB.ComplaintType?.TypeName,
                User = new UserInfoDTO
                  {
                      FullName = complaintDB.User?.FullName,
                      Email = complaintDB.User?.Email
                  }
            };
        }
    }
}
