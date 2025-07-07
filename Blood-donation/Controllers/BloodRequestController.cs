using System.Security.Claims;
using Application.DTO;
using Application.Interface.IService;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blood_donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestController : ControllerBase
    {
        private readonly IBloodRequestservice _service;

        public BloodRequestController(IBloodRequestservice service)
        {
            _service = service;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BloodrequestDto dto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _service.CreateRequestAsync(dto, requesterId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var result = await _service.GetPendingRequestsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRequests()
        {
            var response = await _service.GetAllRequestsAsync();
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("accept/{id}")]
      
        public async Task<IActionResult> Accept(int id)
        {
            var result = await _service.AcceptRequestAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("reject/{id}")]
      
        public async Task<IActionResult> Reject(int id)
        {
            var result = await _service.RejectRequestAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}