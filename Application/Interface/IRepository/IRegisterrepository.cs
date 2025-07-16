using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Model;
namespace Application.Interface.IRepository
{
   public  interface IRegisterrepository
    {
        Task<User> AddDonor(DonorRegistrationDto donorRegistrationDto);
        Task<bool>AddRecipient(RecipientregistrationDto recipientRegistrationDto);
        Task<User> AddHospital(HospitalregistrationDto hospitalregistrationDto);
        Task<User> AddBloodBank(BloodBankRegisterDto bloodBankRegisterDto);



    }
}
