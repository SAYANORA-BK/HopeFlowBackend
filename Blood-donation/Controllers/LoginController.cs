using Application.DTO;
using Application.Interface.IService;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Blood_donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginservice _loginService;

        public LoginController(ILoginservice loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _loginService.LoginAsync(loginDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
