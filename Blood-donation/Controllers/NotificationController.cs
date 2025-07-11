using Application.DTO;
using Application.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blood_donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }



        [HttpPost("CreateRequest")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRequest()
        {

            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            await _notificationService.SendNotificationAsync(userId, "Matching blood request available!");

            return Ok("Matching blood request available!");
        }



        [HttpPost("CreateRequestToDonor")]
        [Authorize(Roles ="Admin")]      
        public async Task<IActionResult> CreateRequesttoDonor()
        {

            int userId = Convert.ToInt32(HttpContext.Items["UserId"]);
            await _notificationService.SendNotificationAsync(userId, "upcoming Bloodcamps!");

            return Ok("Upcoming bloodcamps!");
        }

    }
}
