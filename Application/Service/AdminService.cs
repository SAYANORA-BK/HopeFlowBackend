using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interface.IRepository;
using Application.Interface.IService;
using ommon;

namespace Application.Service
{
    public class AdminService : IAdminService
    {

        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiresponseDto<IEnumerable<AdminuserDto>>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();

            return new ApiresponseDto<IEnumerable<AdminuserDto>>
            {
                StatusCode = 200,
                Message = users.Any() ? "Users fetched successfully." : "No users found.",
                Data = users
            };
        }

        public async Task<ApiresponseDto<object>> VerifyCertificate(int id)
        {
            var success = await _repository.VerifyCertificate(id);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? " verified  successfully." : "Failed to verify ."
            };
        }

        public async Task<ApiresponseDto<object>> BlockUser(int id)
        {


            var AlreadyBlocked = await _repository.IsUserBlocked(id);

            if (AlreadyBlocked)
            {
                return new ApiresponseDto<object>
                {
                    StatusCode = 400,
                    Message = "User is already in blocked list."
                };
            }
            var success = await _repository.BlockUser(id);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "User blocked successfully." : "Failed to block user."
            };
        }

        public async Task<ApiresponseDto<object>> UnBlockUser(int id)
        {
            var AlreadyBlocked = await _repository.IsUserBlocked(id);

            if (!AlreadyBlocked)
            {
                return new ApiresponseDto<object>
                {
                    StatusCode = 400,
                    Message = "User is not in blocked list."
                };
            }
            var success = await _repository.UnBlockUser(id);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "User unblocked successfully." : "Failed to unblock user."
            };
        }

        public async Task<ApiresponseDto<IEnumerable<BloodcampDto>>> GetAllCamps()
        {
            var camps = await _repository.GetAllCamps();

            return new ApiresponseDto<IEnumerable<BloodcampDto>>
            {
                StatusCode = 200,
                Message = camps.Any() ? "Camps fetched successfully." : "No camps found.",
                Data = camps
            };
        }

        public async Task<ApiresponseDto<object>> ApproveCamp(int id)
        {
            var success = await _repository.ApproveCamp(id);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "Camp approved successfully." : "Failed to approve camp."
            };
        }

        public async Task<ApiresponseDto<object>> UpdateCamp(CampDto dto)
        {
            var success = await _repository.UpdateCamp(dto);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "Camp updated successfully." : "Failed to update camp."
            };
        }

        public async Task<ApiresponseDto<AnalyticDto>> GetAnalyticsSummary()
        {
            var data = await _repository.GetAnalyticsSummary();
            return new ApiresponseDto<AnalyticDto>
            {
                StatusCode = 200,
                Message = "Analytics summary fetched successfully.",
                Data = data
            };
        }

     
       
    }
}