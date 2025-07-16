using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interface.IRepository
{
  public  interface IBloodBankRepository
    {
        Task<IEnumerable<BloodInventoryDto>> GetInventory(int bankId);
        Task<int?> GetBloodBankIdByUserId(int userId);
        Task<bool> UpdateInventory(BloodInventoryDto dto, int bankId);
        Task<IEnumerable<CampDto>> GetCamps(int bankId);
        Task<bool> AddCamp(CampDto dto, int bankId);
        Task<bool> DeleteCamp(int campId);
       
    }
}
