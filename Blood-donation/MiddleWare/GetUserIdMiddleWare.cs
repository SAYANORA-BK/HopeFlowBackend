using System.Data;
using System.Security.Claims;
using Microsoft.Data.SqlClient; // or use your existing Dapper context

namespace Blood_donation.MiddleWare
{
    public class GetUserIdMiddleWare
    {
        private readonly ILogger<GetUserIdMiddleWare> _logger;
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public GetUserIdMiddleWare(ILogger<GetUserIdMiddleWare> logger, RequestDelegate next, IConfiguration configuration)
        {
            _logger = logger;
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

                if (idClaim != null)
                {
                    if (int.TryParse(idClaim.Value, out var userId))
                    {
                        try
                        {
                            var connectionString = _configuration.GetConnectionString("DefaultConnection");
                            using var connection = new SqlConnection(connectionString);
                            await connection.OpenAsync();

                            var command = new SqlCommand("SELECT id FROM blood_banks WHERE user_id = @userId", connection);
                            command.Parameters.AddWithValue("@userId", userId);

                            var result = await command.ExecuteScalarAsync();

                            if (result != null && int.TryParse(result.ToString(), out var bankId))
                            {
                                context.Items["UserId"] = bankId;
                                _logger.LogInformation($"Mapped userId {userId} to blood_bank id {bankId}");
                            }
                            else
                            {
                                _logger.LogWarning($"No blood bank found for userId {userId}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error fetching blood bank ID for user.");
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"Invalid userId in token: {idClaim.Value}");
                    }
                }
                else
                {
                    _logger.LogWarning("No NameIdentifier claim found.");
                }
            }

            await _next(context);
        }
    }
}
