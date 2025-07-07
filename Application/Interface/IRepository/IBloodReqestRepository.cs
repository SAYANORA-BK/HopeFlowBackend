using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interface.IRepository
{
   public interface IBloodReqestRepository
    {
        Task<bool> UserExistsAsync(int userId);
        Task<bool> CreateRequestAsync(BloodrequestDto dto, int requesterId);
        Task<IEnumerable<BloodrequestResponseDto>> GetRequestsByStatusAsync(string status);
        Task<bool> UpdateRequestStatusAsync(int requestId, string status);
        Task<IEnumerable<BloodrequestResponseDto>> GetAllRequestsAsync();
    }
}
