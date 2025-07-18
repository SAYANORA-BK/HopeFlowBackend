using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interface.IRepository
{
   public   interface IDonorRepository
    {
        Task<IEnumerable<BloodrequestResponseDto>> GetMatchingRequests(int userId);
        Task<IEnumerable<DonationDto>> GetDonationHistory(int userId);
        Task<DateTime?> GetLastDonationDate(int userId);
        Task<bool> SaveCertificate(int userId, string filePath);
        Task<string> GetCertificatePath(int userId);

    }
}
