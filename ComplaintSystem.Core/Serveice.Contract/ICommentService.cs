using ComplaintSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Core.Serveice.Contract
{
    public interface ICommentService
    {
        Task<bool> AddCommentForEmployeeAsync(AddCommentDTO dto, int ParticipantId);
        Task<bool> AddCommentForUserAsync(AddCommentDTO dto, int UserID);
    }
}
