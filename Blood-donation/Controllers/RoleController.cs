using Application.Interface.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blood_donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
       
        public readonly IRoleservice _roleservice;
        public RoleController(IRoleservice roleservice)
        {
            _roleservice = roleservice;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var role= await  _roleservice.GetAllRole();
            return StatusCode(role.StatusCode, role);

        }

    }
}
