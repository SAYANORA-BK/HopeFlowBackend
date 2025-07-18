using Application.DTO;
using Application.Interface.IRepository;
using Application.Interface.IService;
using ommon;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Service
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IWebHostEnvironment _environment;

        public DonorService(IDonorRepository donorRepository, IWebHostEnvironment environment)
        {
            _donorRepository = donorRepository;
            _environment = environment;
        }

        public async Task<ApiresponseDto<IEnumerable<BloodrequestResponseDto>>> GetMatchingRequests(int userId)
        {
            var data = await _donorRepository.GetMatchingRequests(userId);
            bool hasData = data != null && data.Any();

            return new ApiresponseDto<IEnumerable<BloodrequestResponseDto>>
            {
                StatusCode = 200,
                Message = hasData ? "Requests fetched successfully." : "No requests found.",
                Data = data
            };
        }

        public async Task<ApiresponseDto<IEnumerable<DonationDto>>> GetDonationHistory(int userId)
        {
            var data = await _donorRepository.GetDonationHistory(userId);
            bool hasData = data != null && data.Any();

            return new ApiresponseDto<IEnumerable<DonationDto>>
            {
                StatusCode = 200,
                Message = hasData ? "Donation history fetched successfully." : "No donation history found.",
                Data = data
            };
        }

        public async Task<ApiresponseDto<DateTime?>> GetNextEligibleDate(int userId)
        {
            var lastDonation = await _donorRepository.GetLastDonationDate(userId);
            var nextDate = lastDonation?.AddDays(60);
            return new ApiresponseDto<DateTime?>
            {
                StatusCode = 200,
                Message = nextDate.HasValue ? "Next eligible date calculated." : "No donation record found.",
                Data = nextDate
            };
        }

        public async Task<ApiresponseDto<object>> UploadCertificate(int userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new ApiresponseDto<object>
                {
                    StatusCode = 400,
                    Message = "Invalid file."
                };
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "certificates");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{userId}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var relativePath = $"/certificates/{fileName}";
            var result = await _donorRepository.SaveCertificate(userId, relativePath);

            return new ApiresponseDto<object>
            {
                StatusCode = result ? 200 : 400,
                Message = result ? "Certificate uploaded successfully." : "Failed to upload certificate."
            };
        }

        public async Task<ApiresponseDto<string>> GetCertificate(int userId)
        {
            var path = await _donorRepository.GetCertificatePath(userId);
            return new ApiresponseDto<string>
            {
                StatusCode = string.IsNullOrEmpty(path) ? 404 : 200,
                Message = string.IsNullOrEmpty(path) ? "Certificate not found." : "Certificate retrieved successfully.",
                Data = path
            };
        }
    }
}
