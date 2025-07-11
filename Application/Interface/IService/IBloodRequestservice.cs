using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using ommon;

namespace Application.Interface.IService
{
 public  interface IBloodRequestservice
    {

        Task<ApiresponseDto<object>> CreateRequest(BloodrequestDto dto, int requesterId);
        Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetPendingRequest();
        Task<ApiresponseDto<object>> AcceptRequest(int requestId);
        Task<ApiresponseDto<object>> RejectRequest(int requestId);
        Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetAllRequests();
        Task<ApiresponseDto<object>> EditRequest(int requestId, BloodrequestDto dto);

        Task<ApiresponseDto<string>> DeleteRequestById(int requestId);


    }
}
