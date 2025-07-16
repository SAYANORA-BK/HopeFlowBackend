using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using ommon;

namespace Application.Interface.IService
{
  public  interface IBloodBankService
    {
        Task<ApiresponseDto<IEnumerable<BloodInventoryDto>>> GetInventory(int bankId);
        Task<ApiresponseDto<object>> UpdateInventory(BloodInventoryDto dto,int bankId);
        Task<ApiresponseDto<IEnumerable<CampDto>>> GetCamps(int bankId);
        Task<ApiresponseDto<object>> AddCamp(CampDto dto, int userId);
        Task<ApiresponseDto<object>> DeleteCamp(int campId);
      
    }
}
