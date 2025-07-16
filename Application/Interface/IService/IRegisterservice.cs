using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using ommon;

namespace Application.Interface.IService
{
    public interface IRegisterservice
    {
        Task<ApiresponseDto<object>> AddDonor(DonorRegistrationDto donorRegistrationDto);
        Task<ApiresponseDto<object>> AddRecipient(RecipientregistrationDto recipientRegistrationDto);
        Task<ApiresponseDto<object>> AddHospital(HospitalregistrationDto hospitalregistrationDto);
        Task<ApiresponseDto<object>> AddBloodBank(BloodBankRegisterDto bloodBankRegisterDto);

    }
}
