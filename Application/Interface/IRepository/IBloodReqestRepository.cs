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
        Task<bool> CreateRequest(BloodrequestDto dto, int requesterId);
        Task<IEnumerable<BloodrequestResponseDto>> GetRequestsByStatus(string status);
        Task<bool> UpdateRequestStatus(int requestId, string status);
        Task<IEnumerable<BloodrequestResponseDto>> GetAllRequest();
        Task<bool> EditRequest(int requestId, BloodrequestDto dto);
        Task<bool> DeleteRequest(int id);
    }
}
