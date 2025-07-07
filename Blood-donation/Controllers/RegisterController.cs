using Application.DTO;
using Application.Interface.IService;
using Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ommon;

namespace Blood_donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterservice _registerservice;
        public RegisterController(IRegisterservice registerservice)
        {
            _registerservice = registerservice;
        }
        [HttpPost("Donorregister")]
        public async Task<IActionResult> Register([FromForm] DonorRegistrationDto donorRegistrationDto)

        {
            var donor = await _registerservice.AddDonor( donorRegistrationDto);
            return StatusCode(donor.StatusCode,donor);
        }
        [HttpPost("Recipientregister")]
        public async Task<IActionResult> Register([FromBody] RecipientregistrationDto recipientregistrationDto)

        {
            var recipient = await _registerservice.AddRecipient(recipientregistrationDto);
            return StatusCode(recipient.StatusCode,recipient);
        }
        [HttpPost("Hospitalregister")]
      public async Task<IActionResult> Register([FromForm] HospitalregistrationDto hospitalregistrationDto)
        {
            var Hospital = await _registerservice.AddHospital(hospitalregistrationDto);
            return StatusCode(Hospital.StatusCode, Hospital);

        }


    }
}
