using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interface.IRepository;
using Application.Interface.IService;

using ommon;

namespace Application.Service
{
    public class BloodRequestservice : IBloodRequestservice
    {
        private readonly IBloodReqestRepository _repository;

        public BloodRequestservice(IBloodReqestRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiresponseDto<object>> CreateRequestAsync(BloodrequestDto dto, int requesterId)
        {
            
            var userExists = await _repository.UserExistsAsync(requesterId);
            if (!userExists)
            {
                return new ApiresponseDto<object>
                {
                    StatusCode = 400,
                    Message = "Invalid requester ID. User does not exist."
                };
            }

            var success = await _repository.CreateRequestAsync(dto, requesterId);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "Blood request created successfully." : "Failed to create blood request."
            };
        }

        public async Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetPendingRequestsAsync()
        {
            var result = await _repository.GetRequestsByStatusAsync("Pending");

            if (result == null || !result.Any())
            {
                return new ApiresponseDto<IEnumerable<BloodrequestResponseDto>>
                {
                    StatusCode = 200,
                    Message = "No pending requests.",
                    Data = new List<BloodrequestResponseDto>()
                };
            }

            return new ApiresponseDto<IEnumerable<BloodrequestResponseDto>>
            {
                StatusCode = 200,
                Message = "Pending requests fetched successfully.",
                Data = result
            };
        }


        public async Task<ApiresponseDto<object>> AcceptRequestAsync(int requestId)
        {
            var result = await _repository.UpdateRequestStatusAsync(requestId, "Accepted");
            return new ApiresponseDto<object>
            {
                StatusCode = result ? 200 : 400,
                Message = result ? "Request accepted successfully." : "Failed to accept request."
            };
        }

        public async Task<ApiresponseDto<object>> RejectRequestAsync(int requestId)
        {
            var result = await _repository.UpdateRequestStatusAsync(requestId, "Rejected");
            return new ApiresponseDto<object>
            {
                StatusCode = result ? 200 : 400,
                Message = result ? "Request rejected successfully." : "Failed to reject request."
            };
        }
        public async Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetAllRequestsAsync()
        {
            var result = await _repository.GetAllRequestsAsync();

            if (result == null || !result.Any())
            {
                return new ApiresponseDto<IEnumerable<BloodrequestResponseDto>>
                {
                    StatusCode = 200,
                    Message = "No blood requests found.",
                    Data = new List<BloodrequestResponseDto>()
                };
            }

            return new ApiresponseDto<IEnumerable<BloodrequestResponseDto>>
            {
                StatusCode = 200,
                Message = "All blood requests fetched successfully.",
                Data = result
            };
        }

    }
}
