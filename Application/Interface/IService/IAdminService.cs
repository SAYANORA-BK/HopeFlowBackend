using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using ommon;

namespace Application.Interface.IService
{
   public  interface IAdminService
    {
         Task<ApiresponseDto<IEnumerable<AdminuserDto>>> GetAllUsers();
        Task<ApiresponseDto<object>> VerifyCertificate(int id);
        Task<ApiresponseDto<object>> BlockUser(int id);
        Task<ApiresponseDto<object>> UnBlockUser(int id);
        Task<ApiresponseDto<IEnumerable<CampDto>>> GetAllCamps();
        Task<ApiresponseDto<object>> ApproveCamp(int id);
        Task<ApiresponseDto<object>> UpdateCamp(CampDto dto);
     
        Task<ApiresponseDto<AnalyticDto>> GetAnalyticsSummary();
        

    }
}
