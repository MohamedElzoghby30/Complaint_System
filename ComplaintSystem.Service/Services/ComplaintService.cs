using ComplaintSystem.Core;
using ComplaintSystem.Core.DTOs;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Repository.Contract;
using ComplaintSystem.Core.Serveice.Contract;

namespace ComplaintSystem.Service.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IComplaintTypeRepository _complaintTypeRepository;
      //  private readonly IMapper _mapper;

        public ComplaintService(IComplaintRepository complaintRepository, IComplaintTypeRepository complaintTypeRepository)
        {
            _complaintRepository = complaintRepository;
            _complaintTypeRepository = complaintTypeRepository;
          //  _mapper = mapper;
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateComplaintAsync(AddComplaintDTO complaintDto, int userId)
        {
            if (string.IsNullOrWhiteSpace(complaintDto.Description))
                return (false, new[] { "Description is required." });

            if (!await _complaintRepository.ComplaintTypeExistsAsync(complaintDto.ComplaintTypeID))
                return (false, new[] { "Invalid ComplaintTypeID." });

            var ComplaintTypeDB = await _complaintTypeRepository.GetComplaintTypeByIdAsync(complaintDto.ComplaintTypeID);
            

            var firstWorkflow = ComplaintTypeDB.Workflows.First();
            if (firstWorkflow == null)
                return (false, new[] { "No workflow defined for this complaint type." });

           
            // var complaint = _mapper.Map<Complaint>(complaintDto);
            var complaint = new Complaint()
            {
                Description = complaintDto.Description,
                ComplaintTypeID=complaintDto.ComplaintTypeID,
                UserID = userId,
                Status = ComplaintStatus.Pending,
                CreatAt = DateTime.Now,
                AssignedAt = DateTime.Now,
               AssignedToID = ComplaintTypeDB.Workflows.First().UserId

            };
            complaint.CurrentStepID = firstWorkflow.Id;
            //complaint.UserID = userId;
            //complaint.Status = "Pending";

            // جيب أول Workflow للـ ComplaintType


            await _complaintRepository.AddAsync(complaint);
            return (true, Array.Empty<string>());
        }
        public async Task<PaginatedListCore<Complaint,ComplaintDTO>> GetComplaintsForUserAsync(int userId, ComplaintStatus status=ComplaintStatus.Pending, int pageNumber=1,int PageSize=10 )

        { 
            if (pageNumber <= 0) pageNumber = 1;
            if (PageSize <= 0) PageSize = 10;
           
            var complaints = await _complaintRepository.GetByUserIdAsync(userId, status,pageNumber, PageSize);
           


            var count = await _complaintRepository.GetComlaintsNumByUserIdAsync(userId);

            var totalPages = (int)Math.Ceiling(count / (double)PageSize);
            var hasPreviousPage = pageNumber > 1;
            var hasNextPage = pageNumber < totalPages;


            var paginatedList = new PaginatedListCore<Complaint,ComplaintDTO>
            {
                TotalPages = totalPages,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPreviousPage,
                count = count,
                CurrentPage = pageNumber,

              
            };
            paginatedList.items= complaints.Select(c => new ComplaintDTO {
                Id = c.Id,
                 Status = c.Status.ToString(),
                 Description = c.Description,
                 ComplaintTypeName = c.ComplaintType?.TypeName
                 }).ToList();
            return paginatedList;


        }
        public async Task<PaginatedListCore<Complaint, ComplaintDTO>> AssinComplaint(int userId, ComplaintStatus status = ComplaintStatus.Pending, int pageNumber = 1, int PageSize = 10)

        {
            if (pageNumber <= 0) pageNumber = 1;
            if (PageSize <= 0) PageSize = 10;

            var complaints = await _complaintRepository.Assin(userId, status, pageNumber, PageSize);



            var count = await _complaintRepository.GetComlaintsNumByUserIdAsync(userId);

            var totalPages = (int)Math.Ceiling(count / (double)PageSize);
            var hasPreviousPage = pageNumber > 1;
            var hasNextPage = pageNumber < totalPages;


            var paginatedList = new PaginatedListCore<Complaint, ComplaintDTO>
            {
                TotalPages = totalPages,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPreviousPage,
                count = count,
                CurrentPage = pageNumber,


            };
            paginatedList.items = complaints.Select(c => new ComplaintDTO
            {
                Id = c.Id,
                Status = c.Status.ToString(),
                Description = c.Description,
                ComplaintTypeName = c.ComplaintType?.TypeName
            }).ToList();
            return paginatedList;


        }


        public async Task<ComplaintDTO> GetComplaintAsync(int id, int userId)
        {
            var complaintDB = await _complaintRepository.GetComplaintByIdAsync(id, userId);
            if (complaintDB == null) return null;

            return new ComplaintDTO
            {
                Id = complaintDB.Id,
                Status = complaintDB.Status.ToString(),
                Description = complaintDB.Description,
                ComplaintTypeName = complaintDB.ComplaintType?.TypeName,
                //User = new UserInfoDTO
                //  {
                //      FullName = complaintDB.User?.FullName,
                //      Email = complaintDB.User?.Email
                //  }
            };
        }

        public async Task<Complaint> GetComplaintByIdAsync(int Id,int UserID)
        {
            var complaintDB = await _complaintRepository.GetComplaintByIWithEmployeedAsync(Id, UserID);

            return complaintDB;
        }
        public async Task<bool> UpdateComplaintAsync(Complaint complaint)
        {
            return await _complaintRepository.UpdateComplaintAsync(complaint);
        }
    }
}
