using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interface.IRepository
{
  public  interface IAdminRepository
    {
        Task<IEnumerable<AdminuserDto>> GetAllUsers();
        Task<bool> VerifyCertificate(int id);

        Task<bool> IsUserBlocked(int id);
        Task<bool> BlockUser(int id);
        Task<bool> UnBlockUser(int id);

        Task<IEnumerable<CampDto>> GetAllCamps();
        Task<bool> ApproveCamp(int id);
        Task<bool> UpdateCamp(CampDto dto);
        Task<AnalyticDto> GetAnalyticsSummary();
       
    }
}
