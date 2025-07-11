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
        [Authorize(Roles ="Recipient,Donor")]
        public async Task<IActionResult> Create([FromBody] BloodrequestDto dto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _service.CreateRequest(dto, requesterId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var result = await _service.GetPendingRequest();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Recipient")]
        public async Task<IActionResult> GetAllRequests()
        {
            var response = await _service.GetAllRequests();
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("accept/{id}")]
        [Authorize(Roles = "Donor,Bloodbank")]
        public async Task<IActionResult> Accept(int id)
        {
            var result = await _service.AcceptRequest(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("reject/{id}")]
        [Authorize(Roles = "Donor,Bloodbank")]

        public async Task<IActionResult> Reject(int id)
        {
            var result = await _service.RejectRequest(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("edit/{id}")]
     

  
        public async Task<IActionResult> EditRequest(int id, [FromBody] BloodrequestDto dto)
        {
            var response = await _service.EditRequest(id, dto);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("delete/{id}")]

        public async Task  <IActionResult>Delete(int id)
        {
            var  result= await _service.DeleteRequestById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}