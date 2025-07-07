using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Helpers;
using Application.Interface.IRepository;
using Application.Interface.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ommon;

namespace Application.Service
{
    public class Registerservice : IRegisterservice
    {
        private readonly IRegisterrepository _registerrepository;
        private readonly ICloudinaryService _cloudinaryservice;
        private readonly ICertificaterepository _certificaterepository;
        public Registerservice(IRegisterrepository registerrepository,ICloudinaryService cloudinaryService, ICertificaterepository certificaterepository)
        {
            _registerrepository = registerrepository;
            _cloudinaryservice = cloudinaryService;
            _certificaterepository = certificaterepository;
        }
        public async Task<ApiresponseDto<object>> AddDonor(DonorRegistrationDto donorRegistrationDto)
        {
            try
            {
               
                donorRegistrationDto.hashpassword = PasswordHasher.Hash(donorRegistrationDto.hashpassword);

                var isSaved = await _registerrepository. AddDonor(donorRegistrationDto);
                var image = await _cloudinaryservice.UploadImage(donorRegistrationDto.certificate);
                var certificateupload = await _certificaterepository.AddCertificateAsync(image, isSaved.Id);

                if (isSaved!=null)
                {
                    return new ApiresponseDto<object>
                    {
                        StatusCode= 200,
                        Message = "Donor registered successfully",
                       
                    };
                }
               
                    return new ApiresponseDto<object>
                    {
                        StatusCode =401,
                        Message = "Registration failed",
                       
                    };
                
            }
            catch (Exception ex)
            {
                return new ApiresponseDto<object>
                {
                    StatusCode =500 ,
                    Message = $"Error: {ex.Message}",
                   
                };
            }
        }
      public async   Task<ApiresponseDto<object>> AddRecipient(RecipientregistrationDto recipientRegistrationDto)
        {
            try
            {

                recipientRegistrationDto.hashpassword = PasswordHasher.Hash(recipientRegistrationDto.hashpassword);

                var isSaved = await _registerrepository.AddRecipient(recipientRegistrationDto);

                if (isSaved)
                {
                    return new ApiresponseDto<object>
                    {
                        StatusCode = 200,
                        Message = "Recipient registered successfully",

                    };
                }

                return new ApiresponseDto<object>
                {
                    StatusCode = 401,
                    Message = "Registration failed as a recipient",

                };

            }
            catch (Exception ex)
            {
                return new ApiresponseDto<object>
                {
                    StatusCode = 500,
                    Message = $"Error: {ex.Message}",

                };
            }

        }

     public async   Task<ApiresponseDto<object>> AddHospital(HospitalregistrationDto hospitalregistrationDto)
        {
            try
            {


                hospitalregistrationDto.hashpassword = PasswordHasher.Hash(hospitalregistrationDto.hashpassword);

                var isSaved = await _registerrepository.AddHospital(hospitalregistrationDto);
                var image = await _cloudinaryservice.UploadImage(hospitalregistrationDto.certificate);
                var certificateupload = await _certificaterepository.AddCertificateAsync(image, isSaved.Id);

                if (isSaved!=null)
                {
                    return new ApiresponseDto<object>
                    {
                        StatusCode = 200,
                        Message = "Hospital registered successfully",

                    };
                }

                return new ApiresponseDto<object>
                {
                    StatusCode = 401,
                    Message = "Registration failed as a Hospital",

                };

            }
            catch (Exception ex)
            {
                return new ApiresponseDto<object>
                {
                    StatusCode = 500,
                    Message = $"Error: {ex.Message}",

                };
            }
        }

    }
}
