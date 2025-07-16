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

        public async Task<ApiresponseDto<object>> CreateRequest(BloodrequestDto dto, int requesterId)
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

            var success = await _repository.CreateRequest(dto, requesterId);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "Blood request created successfully." : "Failed to create blood request."
            };
        }

        public async Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetPendingRequest()
        {
            var result = await _repository.GetRequestsByStatus("Pending");

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


        public async Task<ApiresponseDto<object>> AcceptRequest(int requestId)
        {
            var result = await _repository.UpdateRequestStatus(requestId, "Accepted");
            return new ApiresponseDto<object>
            {
                StatusCode = result ? 200 : 400,
                Message = result ? "Request accepted successfully." : "Failed to accept request."
            };
        }

        public async Task<ApiresponseDto<object>> RejectRequest(int requestId)
        {
            var result = await _repository.UpdateRequestStatus(requestId, "Rejected");
            return new ApiresponseDto<object>
            {
                StatusCode = result ? 200 : 400,
                Message = result ? "Request rejected successfully." : "Failed to reject request."
            };
        }
        public async Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetAllRequests()
        {
            var result = await _repository.GetAllRequest();

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
        public async Task<ApiresponseDto<object>> EditRequest(int requestId, BloodrequestDto dto)
        {
            var updated = await _repository.EditRequest(requestId, dto);

            return new ApiresponseDto<object>
            {
                StatusCode = updated ? 200 : 400,
                Message = updated ? "Blood request updated successfully." : "Failed to update blood request."
            };
        }

        public async Task<ApiresponseDto<string>> DeleteRequestById(int requestId)
        {
            
            var status = await _repository.GetRequestStatusById(requestId);

            if (status == null)
            {
                return new ApiresponseDto<string>
                {
                    StatusCode = 404,
                    Message = "Blood request not found.",
                    Data = null
                };
            }

            if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
            {
                return new ApiresponseDto<string>
                {
                    StatusCode = 400,
                    Message = "Rejected request! Not able to delete.",
                    Data = null
                };
            }

      
            var isDeleted = await _repository.DeleteRequest(requestId);

            if (!isDeleted)
            {
                return new ApiresponseDto<string>
                {
                    StatusCode = 404,
                    Message = "Blood request not found or already deleted.",
                    Data = null
                };
            }

            return new ApiresponseDto<string>
            {
                StatusCode = 200,
                Message = "Blood request deleted successfully.",
                Data = $"Request with ID {requestId} has been deleted."
            };
        }

    }


}

