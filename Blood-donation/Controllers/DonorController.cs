using Application.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _donorService;

    public DonorController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [HttpGet("matching-request")]
    [Authorize(Roles ="Donor")]
    public async Task<IActionResult> GetMatchingRequest()
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || !(userIdObj is int userId))
            return Unauthorized();

        var result = await _donorService.GetMatchingRequests(userId);
        return Ok(result);
    }

    [HttpGet("history")]
    [Authorize(Roles ="Donor")]
    public async Task<IActionResult> GetDonationHistory()
    {
        if (!HttpContext.Items.TryGetValue("UserId", out var userIdObj) || !(userIdObj is int userId))
            return Unauthorized();

        var result = await _donorService.GetDonationHistory(userId);
        return Ok(result);
    }
}
