using Azure.Core;
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
        private readonly ICommentRepository _commentRepository;
        public ComplaintService(IComplaintRepository complaintRepository, IComplaintTypeRepository complaintTypeRepository,ICommentRepository commentRepository)
        {
            _complaintRepository = complaintRepository;
            _complaintTypeRepository = complaintTypeRepository;
            _commentRepository = commentRepository;
          //  _mapper = mapper;
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateComplaintAsync(AddComplaintDTO complaintDto, int userId)
        {
            if (string.IsNullOrWhiteSpace(complaintDto.Description))
                return (false, new[] { "Description is required." });

            if (!await _complaintRepository.ComplaintTypeExistsAsync(complaintDto.ComplaintTypeID))
                return (false, new[] { "Invalid ComplaintTypeID." });

            var ComplaintTypeDB = await _complaintTypeRepository.GetComplaintTypeByIdAsync(complaintDto.ComplaintTypeID);
            var firstWorkflow = ComplaintTypeDB.Workflows.FirstOrDefault();

            if (firstWorkflow == null)
                return (false, new[] { "No workflow defined for this complaint type." });

            var complaint = new Complaint()
            {
                Description = complaintDto.Description,
                ComplaintTypeID = complaintDto.ComplaintTypeID,
                UserID = userId,
                Status = ComplaintStatus.Pending,
                CreatAt = DateTime.Now,
                AssignedAt = DateTime.Now,
                AssignedToID = firstWorkflow.UserId,
                Title = complaintDto.Title,
                CurrentStepID = firstWorkflow.Id,
                Attachments = new List<ComplaintAttachment>()
            };

            
            if (complaintDto.Attachments != null && complaintDto.Attachments.Any())
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                foreach (var file in complaintDto.Attachments)
                {
                    if (file.Length > 0)
                    {
                        var allowedExtensions = new[] { ".jpg", ".png", ".pdf", ".docx", ".xlsx" };
                        var extension = Path.GetExtension(file.FileName).ToLower();

                        if (!allowedExtensions.Contains(extension))
                            return (false, new[] { $"File type not allowed: {extension}" });

                        var uniqueFileName = Guid.NewGuid().ToString() + extension;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var fileUrl = $"https://complain.runasp.net/uploads/{uniqueFileName}"; 
                        complaint.Attachments.Add(new ComplaintAttachment
                        {
                            FileUrl = fileUrl
                        });
                    }
                }
            }

            await _complaintRepository.AddAsync(complaint);
            return (true, Array.Empty<string>());
        }

        public async Task<PaginatedListCore<Complaint,ComplaintDTO>> GetComplaintsForUserAsync(int userId, ComplaintStatus status=ComplaintStatus.Pending, int pageNumber=0,int PageSize=0)

        {
            if (pageNumber <= 0) pageNumber = 0;
            if (PageSize <= 0) PageSize = 0;

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
            paginatedList.items= complaints.Select(c => new ComplaintDTO
            {
                Id = c.Id,
                Status = c.Status.ToString(),
                Title = c.Title,
                ComplaintTypeName = c.ComplaintType?.TypeName,
                //Comments = c.CommentsComplainer.Select(cc => new CommentDTO
                //{
                //    Id = cc.Id,
                //    Text = cc.CommentText,
                //    CreatedAt = cc.CreatAt,
                //    UserId = cc.UserId,
                //    UserEmail = cc.User?.Email
                //}).ToList()
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
                Title = c.Title,
                ComplaintTypeName = c.ComplaintType?.TypeName
            }).ToList();
            return paginatedList;


        }


        public async Task<ComplaintDTO> GetComplaintEmployeeAdminAsync(int id, int userId)
        {
            var complaintDB = await _complaintRepository.GetComplaintByIdAsync(id, userId);
            if (complaintDB == null) return null;
            var CommentsDb = await _commentRepository.GetCommentsForComplaintAsync(id);

            return new ComplaintDTO
            {
                Id = complaintDB.Id,
                Status = complaintDB.Status.ToString(),
                Description = complaintDB.Description,
                ComplaintTypeName = complaintDB.ComplaintType?.TypeName,
                Title = complaintDB.Title,
                Comments= CommentsDb.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Text = c.CommentText,
                    CreatedAt = c.CreatAt,
                    UserId = c.UserId,
                    FullName = c.User?.FullName 
                }).ToList(),
                Attachments = complaintDB.Attachments.Select(a => a.FileUrl).ToList()

               
            };
        }
        public async Task<ComplaintDTO> GetComplaintUserAsync(int id, int userId)
        {
            var complaintDB = await _complaintRepository.GetComplaintByIdAsync(id, userId);
            if (complaintDB == null) return null;
            var CommentsDb=  await _commentRepository.GetCommentsForComplaintAsync(id);

            return new ComplaintDTO
            {
                Id = complaintDB.Id,
                Status = complaintDB.Status.ToString(),
                Description = complaintDB.Description,
                ComplaintTypeName = complaintDB.ComplaintType?.TypeName,
                Title = complaintDB.Title,
                Comments = CommentsDb.Where(x=>x.IsForUser==true).Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Text = c.CommentText,
                    CreatedAt = c.CreatAt,
                    UserId = c.UserId,
                    FullName = c.User?.FullName
                }).ToList(),
                Attachments = complaintDB.Attachments.Select(a => a.FileUrl).ToList()


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

        public async Task<bool> UpdateComplaintStatusAsync(UpdateComplaintStatusDTO dto, int userId)
        {
            var complaint = await _complaintRepository.GetComplaintByIdAsync(dto.ComplaintId, userId);

            if (complaint == null)
                return false;

            // Only the assigned user can change the status
            if (complaint.AssignedToID != userId)
                return false;

            // Prevent changing to Escalated here 
            //if (dto.Status == ComplaintStatus.Escalated)
            //    return false;

            complaint.Status = dto.Status;
            return await _complaintRepository.UpdateComplaintAsync(complaint);
        }
    }
}
