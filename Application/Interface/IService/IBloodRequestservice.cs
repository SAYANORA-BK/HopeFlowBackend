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

        Task<ApiresponseDto<object>> CreateRequestAsync(BloodrequestDto dto, int requesterId);
        Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetPendingRequestsAsync();
        Task<ApiresponseDto<object>> AcceptRequestAsync(int requestId);
        Task<ApiresponseDto<object>> RejectRequestAsync(int requestId);
        Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetAllRequestsAsync();


    }
}
