using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interface.IRepository;
using Application.Interface.IService;
using Domain.Model;
using ommon;

namespace Application.Service
{
    public class BloodBankService : IBloodBankService
    {
        private readonly IBloodBankRepository _bloodBankRepository;

   
        public BloodBankService(IBloodBankRepository bloodBankRepository)
        {
            _bloodBankRepository = bloodBankRepository;
        }

        public async Task<ApiresponseDto<IEnumerable<BloodInventoryDto>>> GetInventory(int bankId)
        {
            var data = await _bloodBankRepository.GetInventory(bankId);
            return new ApiresponseDto<IEnumerable<BloodInventoryDto>>
            {
                StatusCode = 200,
                Message = data != null ? "Inventory fetched." : "No inventory found.",
                Data = data
            };
        }

        public async Task<ApiresponseDto<object>> UpdateInventory(BloodInventoryDto dto ,int userId)
        {
            var bankId = await _bloodBankRepository.GetBloodBankIdByUserId(userId);
        
            var success = await _bloodBankRepository.UpdateInventory(dto,userId);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "Inventory updated successfully." : "Failed to update inventory."
            };
        }

        public async Task<ApiresponseDto<IEnumerable<CampDto>>> GetCamps(int bankId)
        {
            var data = await _bloodBankRepository.GetCamps(bankId);
            return new ApiresponseDto<IEnumerable<CampDto>>
            {
                StatusCode = 200,
                Message = data != null ? "Camps fetched." : "No camps found.",
                Data =data
            };
        }
        public async Task<ApiresponseDto<object>> AddCamp(CampDto dto, int userId)
        {
            bool success = await _bloodBankRepository.AddCamp(dto, userId);

            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "Camp added successfully." : "Failed to add camp."
            };
        }


        public async Task<ApiresponseDto<object>> DeleteCamp(int campId)
        {
            var success = await _bloodBankRepository.DeleteCamp(campId);
            return new ApiresponseDto<object>
            {
                StatusCode = success ? 200 : 400,
                Message = success ? "Camp deleted successfully." : "Failed to delete camp."
            };
        }

     
    }
}
