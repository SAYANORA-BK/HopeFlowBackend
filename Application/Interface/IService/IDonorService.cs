using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Microsoft.AspNetCore.Http;
using ommon;

namespace Application.Interface.IService
{
  public  interface IDonorService
    {
        Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetMatchingRequests(int userId);
        Task<ApiresponseDto<IEnumerable<DonationDto>>> GetDonationHistory(int userId);
        Task<ApiresponseDto<DateTime?>> GetNextEligibleDate(int userId);
        Task<ApiresponseDto<object>> UploadCertificate(int userId, IFormFile file);
        Task<ApiresponseDto<string>> GetCertificate(int userId);

    }
}
