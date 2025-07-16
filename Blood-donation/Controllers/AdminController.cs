using Application.DTO;
using Application.Interface.IService;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ommon;

namespace Blood_donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("users")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ApiresponseDto<IEnumerable<AdminuserDto>>>> GetAllUsers()
        {
            var response = await _adminService.GetAllUsers();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("verify-certificate/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiresponseDto<object>>> VerifyCertificate(int id)
        {
            var response = await _adminService.VerifyCertificate(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("block-user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiresponseDto<object>>> BlockUser(int id)
        {
            var response = await _adminService.BlockUser(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("unblock-user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiresponseDto<object>>> unBlockUser(int id)
        {
            var response = await _adminService.UnBlockUser(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("camps")]
        [Authorize(Roles ="Admin,Donor")]
        public async Task<ActionResult<ApiresponseDto<IEnumerable<BloodcampDto>>>> GetAllCamps()
        {
            var response = await _adminService.GetAllCamps();
            return StatusCode(response.StatusCode, response);
        }

        
        [HttpGet("analytics")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiresponseDto<AnalyticDto>>> GetAnalyticsSummary()
        {
            var response = await _adminService.GetAnalyticsSummary();
            return StatusCode(response.StatusCode, response);
        }

        
    }
}
