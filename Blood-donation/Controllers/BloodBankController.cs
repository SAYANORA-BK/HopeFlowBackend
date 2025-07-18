using Application.DTO;
using Application.Interface.IService;
using Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blood_donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBankController : ControllerBase
    {
        private readonly IBloodBankService _bloodbanbankservice;

        public BloodBankController(IBloodBankService bloodbankService)
        {
            _bloodbanbankservice = bloodbankService;
        }
        [HttpGet("inventory")]
        public async Task<IActionResult> GetInventory()
        {
            int bankId = Convert.ToInt32(HttpContext.Items["UserId"]);
            var response = await _bloodbanbankservice.GetInventory(bankId);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("inventory/update")]
        public async Task<IActionResult> UpdateInventory([FromBody] BloodInventoryDto dto)
        {
            int bankId = Convert.ToInt32(HttpContext.Items["UserId"]);
            var response = await _bloodbanbankservice.UpdateInventory(dto,bankId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("camps/{bankId}")]
        public async Task<IActionResult> GetCamps(int bankId)
        {
            var response = await _bloodbanbankservice.GetCamps(bankId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("camps/add")]
        public async Task<IActionResult> AddCamp([FromBody] CampDto dto)
        {
            try
            {
                if (!HttpContext.Items.ContainsKey("UserId"))
                    return Unauthorized(new { Message = "User not authorized." });

                int bankId = Convert.ToInt32(HttpContext.Items["UserId"]);

                var response = await _bloodbanbankservice.AddCamp(dto, bankId);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }

        [HttpDelete("camps/{campId}")]
        public async Task<IActionResult> DeleteCamp(int campId)
        {
            var response = await _bloodbanbankservice.DeleteCamp(campId);
            return StatusCode(response.StatusCode, response);
        }

        
    }
}
